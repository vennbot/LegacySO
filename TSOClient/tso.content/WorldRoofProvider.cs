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
using FSO.Content.Codecs;
using FSO.Content.Framework;
using FSO.Content.Model;
using System.Text.RegularExpressions;

namespace FSO.Content
{
    public class WorldRoofProvider : FileProvider<ITextureRef>
    {
        public WorldRoofProvider(Content contentManager) : base(contentManager, new TextureCodec(new uint[] { }, true), 
            new Regex(contentManager.TS1? "GameData/Roofs/.*\\.bmp" : "housedata/roofs/.*\\.jpg"))
        {
            UseTS1 = contentManager.TS1;
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public string IDToName(int id)
        {
            return Items[id].Name;
        }

        public int NameToID(string name)
        {
            return Items.FindIndex(x => x.Name == name);
        }
    }
}
