
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
using FSO.Common;
using System.Collections.Generic;
using System.IO;

namespace FSO.Client
{
    public class GlobalSettings : IniConfig
    {
        private static GlobalSettings defaultInstance;

        public static GlobalSettings Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new GlobalSettings(Path.Combine(FSOEnvironment.UserDir, "config.ini"));
                    if (defaultInstance.DPIScaleFactor > 4 || defaultInstance.DPIScaleFactor == 0)
                        defaultInstance.DPIScaleFactor = 1; //sanity check
                    if (defaultInstance.ChatWindowsOpacity == 0 || defaultInstance.ChatWindowsOpacity > 1)
                        defaultInstance.ChatWindowsOpacity = 1; //sanity check
                    if (defaultInstance.GameEntryUrl == "http://api.freeso.org")
                    {
                        defaultInstance.GameEntryUrl = "https://api.legacyso.org";
                        defaultInstance.CitySelectorUrl = "https://api.legacyso.org";
                    }

                }
                return defaultInstance;
            }
        }

        public GlobalSettings(string path) : base(path) { }

        private Dictionary<string, string> _DefaultValues = new Dictionary<string, string>()
        {
            { "ShowHints", "true"},
            { "CurrentLang", "english" },
            { "ClientVersion", "0"},
            { "DebugEnabled", "false"},
            { "ScaleUI", "false"},
            { "CityShadows", "false"},
            { "ShadowQuality", "2048"},
            { "SmoothZoom", "true"},
            { "AntiAlias", "0"},
            { "EdgeScroll", "true"},
            { "Lighting", "true"},
            { "FXVolume", "10"},
            { "MusicVolume", "10"},
            { "VoxVolume", "10"},
            { "AmbienceVolume", "1"},
            { "StartupPath", ""},
            { "DocumentsPath", ""},
            { "Windowed", "true"},
            { "GraphicsWidth", "1024"},
            { "GraphicsHeight", "768"},
            { "LastUser", ""},
            { "SkipIntro", "true"},
            { "DebugHead", "0"},
            { "DebugBody", "0"},
            { "DebugGender", "true"},
            { "DebugSkin", "0"},
            { "LanguageCode", "1"},
            { "SurroundingLotMode", "2" },

            { "UseCustomServer", "true" },
            { "GameEntryUrl", "https://api.legacyso.org" },
            { "CitySelectorUrl", "https://api.legacyso.org" },

            { "TargetRefreshRate", "60" },

            { "TTSMode", "1" }, //disable/allow/force
            { "CompatState", "-1" },

            { "TS1HybridPath", "D:/Games/The Sims/" },
            { "TS1HybridEnable", "false" },

            { "Shadows3D", "false" },
            { "CitySkybox", "true" },

            { "LightingMode", "-1" },
            { "Weather", "true" },
            { "DirectionalLight3D", "true" },
            { "DPIScaleFactor", "1" },
            { "TexCompression", "0" },

            { "ChatColor", "0" }, //uint packed color. 0 means choose random
            { "ChatTTSPitch", "0" }, //-100 to 100
            { "ChatOnlyEmoji", "false" },
            { "ChatShowTimestamp", "false" },
            { "ChatSizeX", "400" },
            { "ChatSizeY", "255" },
            {"ChatLocationX", "20" },
            {"ChatLocationY", "20" },
            {"ChatDeltaScale", "8" },
            { "ChatWindowsOpacity", "0.8" },

            { "ComplexShaders", "false" },
            { "GlobalGraphicsMode", "0" }, //2d, 2d hybrid, 3d
            { "EnableTransitions", "true" }
        };
        public override Dictionary<string, string> DefaultValues
        {
            get { return _DefaultValues; }
            set { _DefaultValues = value; }
        }

        public string CurrentLang { get; set; }
        public string ClientVersion { get; set; }
        public bool CityShadows { get; set; }
        public int ShadowQuality { get; set; }
        public bool SmoothZoom { get; set; }
        public int AntiAlias { get; set; }
        public bool EdgeScroll { get; set; }
        public bool Lighting { get; set; }
        public byte FXVolume { get; set; }
        public byte MusicVolume { get; set; }
        public byte VoxVolume { get; set; }
        public byte AmbienceVolume { get; set; }
        public string StartupPath { get; set; }
        public string DocumentsPath { get; set; }
        public bool Windowed { get; set; }
        public int GraphicsWidth { get; set; }
        public int GraphicsHeight { get; set; }
        public string LastUser { get; set; }
        public bool SkipIntro { get; set; }
        public ulong DebugHead { get; set; }
        public ulong DebugBody { get; set; }
        public bool DebugGender { get; set; }
        public int DebugSkin { get; set; }
        public byte LanguageCode { get; set; }

        public bool UseCustomServer { get; set; }
        public string GameEntryUrl { get; set; }
        public string CitySelectorUrl { get; set; }

        public int TargetRefreshRate { get; set; }
        public int TTSMode { get; set; } //disable/allow/force
        public int SurroundingLotMode { get; set; }
        public int CompatState { get; set; }

        public string TS1HybridPath { get; set; }
        public bool TS1HybridEnable { get; set; }

        public bool Shadows3D { get; set; }
        public bool CitySkybox { get; set; }

        public int LightingMode { get; set; }

        public bool Weather { get; set; }
        public bool DirectionalLight3D { get; set; }
        public float DPIScaleFactor { get; set; }
        public int TexCompression { get; set; } //first bit on/off, second bit is user defined or auto.

        public uint ChatColor { get; set; }
        public int ChatTTSPitch { get; set; }
        public int ChatOnlyEmoji { get; set; }
        public bool ChatShowTimestamp { get; set; }
        public float ChatSizeX { get; set; }
        public float ChatSizeY { get; set; }
        public float ChatLocationX { get; set; }
        public float ChatLocationY { get; set; }
        public int ChatDeltaScale { get; set; }
        public float ChatWindowsOpacity { get; set; }

        public bool ComplexShaders { get; set; }
        public int GlobalGraphicsMode { get; set; }
        public bool EnableTransitions { get; set; }

        public static int TARGET_COMPAT_STATE = 2;
    }
}
