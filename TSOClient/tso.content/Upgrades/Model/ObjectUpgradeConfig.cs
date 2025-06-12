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

namespace FSO.Content.Upgrades.Model
{
    public class ObjectUpgradeConfig
    {
        /// <summary>
        /// The GUID for the target object.
        /// </summary>
        public string GUID;

        /// <summary>
        /// The initial level this object should start at.
        /// </summary>
        public int Level;

        /// <summary>
        /// The upgrade level this object should go up to.
        /// </summary>
        public int? Limit;

        /// <summary>
        /// If true, the initial object level uses its builtin tuning rather than the replacement.
        /// </summary>
        public bool? Special;

        /// <summary>
        /// If true, the object's init function will be run again when upgrading to this level.
        /// </summary>
        public bool Reinit;

        public void Load(int version, BinaryReader reader)
        {
            GUID = reader.ReadString();
            Level = reader.ReadInt32();
            Limit = reader.ReadInt32();
            if (Limit < 0) Limit = null;
            Special = reader.ReadBoolean();
            Reinit = reader.ReadBoolean();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(GUID);
            writer.Write(Level);
            writer.Write(Limit ?? -1);
            writer.Write(Special ?? false);
            writer.Write(Reinit);
        }
    }
}
