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
    public class UpgradeSubstitution
    {
        /// <summary>
        /// Tuning value to replace. Format: table:id... eg 4097:0. Can also point to a group: "G0".
        /// </summary>
        public string Old = "0:0";

        /// <summary>
        /// Target value. Constants are prefixed with C, eg C4097:1... and Values are prefixed with V, eg V25, V0, V-600.
        /// </summary>
        public string New = "V0";

        public void Load(int version, BinaryReader reader)
        {
            Old = reader.ReadString();
            New = reader.ReadString();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Old);
            writer.Write(New);
        }
    }
}
