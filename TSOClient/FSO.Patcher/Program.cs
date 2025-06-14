
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FSO.Patcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var path = UpdatePath();
            var platform = Environment.OSVersion.Platform;
            if (platform == PlatformID.Unix || platform == PlatformID.MacOSX)
            {
                //console only application
                var patcher = new CLIPatcher(path, args);
                patcher.Begin();
            }
            else
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                Directory.SetCurrentDirectory(baseDir);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormsPatcher(path, args));
            }
        }

        static List<string> UpdatePath()
        {
            try
            {
                var files = Directory.GetFiles("PatchFiles/");
                return files.Where(x => x.EndsWith(".zip") && !x.EndsWith("patch.zip")).OrderBy(x => {
                    var match = Regex.Match(x, @"\d+").Value ?? "200";
                    if (match == "") match = "200";
                    return int.Parse(match);
                    }
                ).ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
