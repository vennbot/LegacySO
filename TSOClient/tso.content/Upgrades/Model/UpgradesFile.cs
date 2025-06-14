
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
    public class UpgradesFile
    {
        public int Version = 2;
        public List<UpgradeIff> Files = new List<UpgradeIff>();

        public void Load(BinaryReader reader)
        {
            Version = reader.ReadInt32();

            Files.Clear();

            var fileCount = reader.ReadInt32();
            for (int i = 0; i < fileCount; i++)
            {
                var file = new UpgradeIff();
                file.Load(Version, reader);
                Files.Add(file);
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write(Files.Count);
            foreach (var file in Files) file.Save(writer);
        }
    }
}
