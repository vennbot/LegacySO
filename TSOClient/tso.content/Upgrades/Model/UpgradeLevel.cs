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
    public class UpgradeLevel
    {
        /// <summary>
        /// The name of this upgrade level. If it is an integer, the name and description are taken from the default string set.
        /// </summary>
        public string Name = "0";

        /// <summary>
        /// The price of this upgrade level. If it starts with $, it is a literal price. If it starts with R, it is a RELATIVE price.
        /// If neither, it is an 8 character hex GUID pointing to an object - whose catalog price should be used.
        /// </summary>
        public string Price = "$0";

        /// <summary>
        /// The ads of this upgrade level. A semicolon separated list of motive:value. (hunger, energy, comfort, bladder, hygiene, social, fun, room)
        /// Also includes special ads such as skilling (which can be given value 1)
        /// Can also be an 8 character hex GUID pointing to an object - whose ads should be used.
        /// </summary>
        public string Ad = "";

        /// <summary>
        /// A description for this upgrade level, shown in the editor. If the level is custom, this description will also be shown ingame.
        /// </summary>
        public string Description = "";

        /// <summary>
        /// Hidden upgrade levels exist, but cannot be upgraded to normally. Use for limited edition upgrades.
        /// </summary>
        public bool? Hidden;

        /// <summary>
        /// A list of tuning substitutions for this level. For objects on this level, these substituions replace tuning values that govern how quickly
        /// they increase motives, skills or other benefits. The tuning values that are replaced can be from either OTF or BHAV chunks.
        /// </summary>
        public List<UpgradeSubstitution> Subs = new List<UpgradeSubstitution>();

        public void Load(int version, BinaryReader reader)
        {
            Name = reader.ReadString();
            Price = reader.ReadString();
            Ad = reader.ReadString();
            Description = reader.ReadString();
            Hidden = reader.ReadBoolean();

            var subCount = reader.ReadInt32();
            Subs.Clear();
            for (int i=0; i<subCount; i++)
            {
                var sub = new UpgradeSubstitution();
                sub.Load(version, reader);
                Subs.Add(sub);
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(Price);
            writer.Write(Ad);
            writer.Write(Description);
            writer.Write(Hidden ?? false);

            writer.Write(Subs.Count);
            foreach (var sub in Subs)
            {
                sub.Save(writer);
            }
        }
    }
}
