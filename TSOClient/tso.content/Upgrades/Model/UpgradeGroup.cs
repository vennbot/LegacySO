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
using System.IO;

namespace FSO.Content.Upgrades.Model
{
    public class UpgradeGroup
    {
        /// <summary>
        /// Group name. Not used when applying upgrades, but useful for creating and copying them around.
        /// </summary>
        public string Name = "";

        /// <summary>
        /// Tuning entries affected by this upgrade group. In format "table:id", similar to substitution value.
        /// </summary>
        public List<string> Tuning = new List<string>();

        /// <summary>
        /// If it exists, the default value to set this group to. Format same as substitutions, "V0" for value, "C4096:1" for constant.
        /// 
        /// This is applied REGARDLESS of the object's "use original tuning" status.
        /// If an object uses original tuning but would have an upgrade specific value for this group, it would fall back to this default
        /// instead of the existing value. Useful for replacements for non-upgrade values.
        /// </summary>
        public string DefaultValue;

        public void Load(int version, BinaryReader reader)
        {
            Name = reader.ReadString();
            Tuning.Clear();
            var tuningCount = reader.ReadInt32();
            for (int i=0; i < tuningCount; i++)
            {
                Tuning.Add(reader.ReadString());
            }
            if (reader.ReadBoolean())
            {
                DefaultValue = reader.ReadString();
            }
            else
            {
                DefaultValue = null;
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(Tuning.Count);
            foreach (var tuning in Tuning)
            {
                writer.Write(tuning);
            }
            writer.Write(DefaultValue != null);
            if (DefaultValue != null)
            {
                writer.Write(DefaultValue);
            }
        }
    }
}
