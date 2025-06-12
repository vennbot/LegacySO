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
using FSO.Files.Utils;

namespace FSO.Files.RC
{
    public class DGRPRCParams
    {
        public bool[] Rotations = new bool[] { true, true, true, true };
        public bool DoorFix; //depending on subtile, disable certain rotations to fix door.
        public bool CounterFix; //extrapolate z on sides of counter to the edge of the tile.

        public int StartDGRP;
        public int EndDGRP;
        public bool BlenderTweak;
        public bool Simplify = true;

        public bool InRange(int dgrp)
        {
            return ((StartDGRP == EndDGRP && EndDGRP == 0) || (dgrp >= StartDGRP && dgrp <= EndDGRP));
        }

        public DGRPRCParams() { }
        public DGRPRCParams(IoBuffer io, int version)
        {
            Rotations = new bool[4];
            for (int i = 0; i < 4; i++) Rotations[i] = io.ReadByte() > 0;
            DoorFix = io.ReadByte() > 0;
            CounterFix = io.ReadByte() > 0;
            StartDGRP = io.ReadInt32();
            EndDGRP = io.ReadInt32();
            BlenderTweak = io.ReadByte() > 0;
            Simplify = io.ReadByte() > 0;
        }

        public void Save(IoWriter io)
        {
            foreach (var rotation in Rotations) io.WriteByte((byte)(rotation ? 1 : 0));
            io.WriteByte((byte)(DoorFix ? 1 : 0));
            io.WriteByte((byte)(CounterFix ? 1 : 0));
            io.WriteInt32(StartDGRP);
            io.WriteInt32(EndDGRP);
            io.WriteByte((byte)(BlenderTweak ? 1 : 0));
            io.WriteByte((byte)(Simplify ? 1 : 0));
        }
    }
}
