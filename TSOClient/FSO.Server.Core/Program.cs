// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
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
using FSO.Server.Api.Core.Services;
using FSO.Server.Common;
using FSO.Server.Servers.UserApi;
using System.Collections.Specialized;
using System.Text;

namespace FSO.Server.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Content.Model.AbstractTextureRef.ImageFetchFunction = CoreImageLoader.SoftImageFetch;
            UserApi.CustomStartup = StartWebApi;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1252 = Encoding.GetEncoding(1252);

            FSO.Server.Program.Main(args);
        }

        public static IAPILifetime StartWebApi(UserApi api, string url)
        {
            var config = api.Config;
            var userApiConfig = config.Services.UserApi;
            var settings = new NameValueCollection();
            settings.Add("maintenance", userApiConfig.Maintenance.ToString());
            settings.Add("authTicketDuration", userApiConfig.AuthTicketDuration.ToString());
            settings.Add("regkey", userApiConfig.Regkey);
            settings.Add("secret", config.Secret);
            settings.Add("updateUrl", userApiConfig.UpdateUrl);
            settings.Add("cdnUrl", userApiConfig.CDNUrl);
            settings.Add("connectionString", config.Database.ConnectionString);
            settings.Add("NFSdir", config.SimNFS);
            settings.Add("smtpHost", userApiConfig.SmtpHost);
            settings.Add("smtpUser", userApiConfig.SmtpUser);
            settings.Add("smtpPassword", userApiConfig.SmtpPassword);
            settings.Add("smtpPort", userApiConfig.SmtpPort.ToString());
            settings.Add("useProxy", userApiConfig.UseProxy.ToString());
            settings.Add("updateID", config.UpdateID?.ToString() ?? "");
            settings.Add("branchName", config.UpdateBranch);

            var api2 = new FSO.Server.Api.Core.Api();
            api2.Init(settings);

            if (userApiConfig.AwsConfig != null)
            {
                api2.AddonUploader = new AWSUpdateUploader(userApiConfig.AwsConfig);
            }
            else
            {
                api2.AddonUploader = new FilesystemUpdateUploader(userApiConfig.FilesystemConfig ?? new Common.Config.FilesystemConfig());
            }

            if (userApiConfig.GithubConfig != null)
            {
                api2.UpdateUploader = new GithubUpdateUploader(userApiConfig.GithubConfig);
            }
            else
            {
                api2.UpdateUploader = api2.AddonUploader;
            }

            api2.Github = userApiConfig.GithubConfig;
            api.SetupInstance(api2);
            api2.HostPool = api.GetGluonHostPool();
            
            return FSO.Server.Api.Core.Program.RunAsync(new string[] { url });
        }
    }
}
