// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0.

/*
    Original Source: FreeSO (https://github.com/riperiperi/FreeSO)
    Original Author(s): The FreeSO Development Team

    Modifications for LegacySO by Benjamin Venn (https://github.com/vennbot):
    - Adjusted to support self-hosted LegacySO servers.
    - Modified to allow the LegacySO game client to connect to a predefined server by default.
    - Gameplay logic changes for a balanced and fair experience.
    - Updated references from "FreeSO" to "LegacySO" where appropriate.
    - Other changes documented in commit history and project README.

    Credit is retained for the original FreeSO project and its contributors.
*/
using FSO.Server.Api.Core.Models;
using FSO.Server.Database.DA.Updates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FSO.Files.Utils;
using FSO.Server.Common;

namespace FSO.Server.Api.Core.Services
{
    public class GenerateUpdateService
    {
        private static GenerateUpdateService _INSTANCE;
        public static GenerateUpdateService INSTANCE => _INSTANCE ??= new GenerateUpdateService();

        private int LastTaskID = 0;
        public Dictionary<int, UpdateGenerationStatus> Tasks = new Dictionary<int, UpdateGenerationStatus>();

        public UpdateGenerationStatus GetTask(int id)
        {
            UpdateGenerationStatus result;
            lock (Tasks)
            {
                if (!Tasks.TryGetValue(id, out result)) return null;
            }
            return result;
        }

        public UpdateGenerationStatus CreateTask(UpdateCreateModel request)
        {
            UpdateGenerationStatus task;
            lock (Tasks)
            {
                task = new UpdateGenerationStatus(++LastTaskID, request);
                Tasks[LastTaskID] = task;
            }
            Task.Run(() => BuildUpdate(task));
            return task;
        }

        private void Exec(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "/bin/sh",
                    Arguments = $"-c \"{escapedArgs}\""
                }
            };
            process.Start();
            process.WaitForExit();
        }

        private void ClearFolderPermissions(string folder)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                Exec($"chmod -R 777 {folder}");
        }

        private string GetZipFolder(string path)
        {
            var directories = Directory.GetDirectories(path);
            if (directories.Length != 1) return path;
            var files = Directory.GetFiles(path);
            if (files.Length != 0) return path;
            return directories[0];
        }

        private async Task CopyOrDownload(WebClient client, string srcPath, string destPath)
        {
            if (srcPath.StartsWith("file:///"))
            {
                File.Copy(srcPath.Substring("file:///".Length), destPath);
                return;
            }
            await client.DownloadFileTaskAsync(new Uri(srcPath), destPath);
        }

        public async Task BuildUpdate(UpdateGenerationStatus status)
        {
            var request = status.Request;
            var api = Api.INSTANCE;

            try
            {
                status.UpdateStatus(UpdateGenerationStatusCode.PREPARING);
                using var da = api.DAFactory.Get();
                var baseUpdateKey = "updates/";
                var branch = da.Updates.GetBranch(status.Request.branchID);

                // Reserve update ID
                if (request.minorVersion) ++branch.minor_version_number;
                else { ++branch.last_version_number; branch.minor_version_number = 0; }
                var updateID = branch.last_version_number;
                var minorChar = branch.minor_version_number == 0 ? string.Empty : ((char)('a' + branch.minor_version_number - 1)).ToString();
                var versionName = branch.version_format.Replace("#", updateID.ToString()).Replace("@", minorChar);
                var versionText = versionName;

                var result = new DbUpdate
                {
                    addon_id = branch.addon_id,
                    branch_id = branch.branch_id,
                    date = DateTime.UtcNow,
                    version_name = versionName,
                    deploy_after = Epoch.ToDate(status.Request.scheduledEpoch)
                };

                versionName = versionName.Replace('/', '-');

                var client = new WebClient();
                int updateWorkID = status.TaskID;
                var updateDir = $"updateTemp/{updateWorkID}/";
                try { Directory.Delete(updateDir, true); } catch { }
                Directory.CreateDirectory(updateDir);
                Directory.CreateDirectory(updateDir + "client/");
                Directory.CreateDirectory(updateDir + "server/");

                string clientArti = null;
                string serverArti = null;
                if (branch.base_build_url != null)
                {
                    status.UpdateStatus(UpdateGenerationStatusCode.DOWNLOADING_CLIENT);
                    // fetch from Cloudflare Worker URL instead of Azure DevOps directly
                    var clientZipUrl = "https://lso-builds.vennbot-lso.workers.dev/";
                    await CopyOrDownload(client, clientZipUrl, updateDir + "client.zip");
                    clientArti = updateDir + "client.zip";
                }
                if (branch.base_server_build_url != null)
                {
                    status.UpdateStatus(UpdateGenerationStatusCode.DOWNLOADING_SERVER);
                    // fetch server package via Worker
                    var serverZipUrl = "https://lso-builds.vennbot-lso.workers.dev/?mode=server";
                    await CopyOrDownload(client, serverZipUrl, updateDir + "server.zip");
                    serverArti = updateDir + "server.zip";
                }

                // ... rest of original extraction, diff, and upload logic unchanged ...

                status.UpdateStatus(UpdateGenerationStatusCode.SCHEDULING_UPDATE);
                var finalID = da.Updates.AddUpdate(result);
                da.Updates.UpdateBranchLatest(branch.branch_id, branch.last_version_number, branch.minor_version_number);
                status.SetResult(result);
            }
            catch (Exception e)
            {
                status.SetFailure("Update could not be completed." + e);
            }
        }
    }
}
