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
using System.Collections.Generic;

namespace FSO.Server.Watchdog
{
    public class Config : IniConfig
    {
        private static Config defaultInstance;

        public static Config Default
        {
            get
            {
                if (defaultInstance == null)
                    defaultInstance = new Config("watchdog.ini");
                return defaultInstance;
            }
        }

        public Config(string path) : base(path) { }

        private Dictionary<string, string> _DefaultValues = new Dictionary<string, string>()
        {
            { "ManifestDownload", "True" },

            { "UseTeamCity", "False" },
            { "TeamCityUrl", "http://servo.freeso.org" },
            { "TeamCityProject", "FreeSO_TsoClient" },
            { "Branch", "feature/server-rebuild" },

            { "NormalUpdateUrl", "https://dl.dropboxusercontent.com/u/12239448/FreeSO/devserver.zip" },
        };
        public override Dictionary<string, string> DefaultValues
        {
            get { return _DefaultValues; }
            set { _DefaultValues = value; }
        }

        public bool ManifestDownload { get; set; }
        public bool UseTeamCity { get; set; }
        public string TeamCityUrl { get; set; }
        public string TeamCityProject { get; set; }
        public string Branch { get; set; }

        public string NormalUpdateUrl { get; set; }
    }
}
