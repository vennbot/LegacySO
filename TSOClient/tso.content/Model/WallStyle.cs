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
using FSO.Files.Formats.IFF.Chunks;

namespace FSO.Content.Model
{
    public class WallStyle
    {
        public ushort ID;
        public string Name;
        public string Description;
        public int Price;
        public SPR WallsUpNear;
        public SPR WallsUpMedium;
        public SPR WallsUpFar;
        //for most fences, the following will be null. This means to use the ones above when walls are down.
        public SPR WallsDownNear;
        public SPR WallsDownMedium;
        public SPR WallsDownFar;

        public bool IsDoor; // Set at runtime for dynamic wall styles.
    }
}
