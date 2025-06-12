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
using System.IO;

namespace FSO.Server.Common
{
    public class ServerVersion
    {
        public string Name;
        public string Number;
        public int? UpdateID;

        public static ServerVersion Get()
        {
            var result = new ServerVersion()
            {
                Name = "unknown",
                Number = "0"
            };

            if (File.Exists("version.txt"))
            {
                using (StreamReader Reader = new StreamReader(File.Open("version.txt", FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    var str = Reader.ReadLine();
                    var split = str.LastIndexOf('-');

                    result.Name = str;
                    if (split != -1)
                    {
                        result.Name = str.Substring(0, split);
                        result.Number = str.Substring(split + 1);
                    }
                }
            }

            if (File.Exists("updateID.txt"))
            {
                var stringID = File.ReadAllText("updateID.txt");
                int id;
                if (int.TryParse(stringID, out id))
                {
                    result.UpdateID = id;
                }
            }

            return result;
        }
    }
}
