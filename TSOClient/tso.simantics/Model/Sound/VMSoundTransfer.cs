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
namespace FSO.SimAntics.Model.Sound
{
    public class VMSoundTransfer
    {
        public short SourceID; //only copy on ID+GUID match. In future, match on persist too?
        public uint SourceGUID;
        public VMSoundEntry SFX;

        public VMSoundTransfer(short id, uint guid, VMSoundEntry sfx)
        {
            SourceID = id;
            SourceGUID = guid;
            SFX = sfx;
        }
    }
}
