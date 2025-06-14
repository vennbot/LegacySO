
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
namespace FSO.Files.HIT
{
    public class Patch
    {
        public string Name;
        public string Filename;
        public bool Looped;
        public bool Piano;

        public uint FileID; //patches are stubbed out in TSO.
        public bool TSO;

        public Patch(uint id)
        {
            FileID = id;
            TSO = true;
        }

        public Patch(string patchString)
        {
            var elems = patchString.Split(',');
            if (elems.Length > 1) Name = elems[1];
            if (elems.Length > 2) Filename = elems[2].Substring(1, elems[2].Length-2).Replace('\\', '/');
            if (elems.Length > 3) Looped = elems[3] != "0";
            if (elems.Length > 4) Piano = elems[4] != "0";
        }
    }
}
