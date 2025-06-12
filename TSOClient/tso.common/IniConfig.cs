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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FSO.Common
{
    public abstract class IniConfig
    {
        private string ActivePath;

        public abstract Dictionary<string, string> DefaultValues
        {
            get; set;
        }

        private void SetValue(string key, string value)
        {
            var prop = this.GetType().GetProperty(key);
            if (prop != null)
            {
                try
                {
                    if (prop.PropertyType != typeof(string))
                        prop.SetValue(this, Convert.ChangeType(value, prop.PropertyType, CultureInfo.InvariantCulture));
                    else prop.SetValue(this, value);
                }
                catch (Exception) { }
            }
        }

        public IniConfig(string path)
        {
            ActivePath = path;
            Load();
        }

        public void Load()
        {
            //assume default values for all unset properties
            foreach (var pair in DefaultValues)
            {
                SetValue(pair.Key, pair.Value);
            }

            if (!File.Exists(ActivePath))
            {
                Save();
            } else
            {
                var lines = File.ReadAllLines(ActivePath);
                foreach (var line in lines)
                {
                    var clean = line.Trim();
                    if (clean.Length == 0 || clean[0] == '#' || clean[0] == '[') continue;
                    var split = clean.IndexOf('=');
                    if (split == -1) continue; //?
                    var prop = clean.Substring(0, split).Trim();
                    var value = clean.Substring(split+1).Trim();

                    SetValue(prop, value);
                }
            }
        }

        public void Save()
        {
            try
            {
                using (var stream = new StreamWriter(File.Open(ActivePath, FileMode.Create, FileAccess.Write)))
                {
                    stream.WriteLine("# LegacySO Settings File. Properties are self explanatory.");
                    var props = this.GetType().GetProperties();
                    foreach (var prop in props)
                    {
                        if (prop.Name == "Default" || prop.Name == "DefaultValues") continue;
                        stream.WriteLine(prop.Name + "=" + Convert.ToString(prop.GetValue(this), CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
