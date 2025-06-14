
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
using System;
using FSO.SimAntics.Engine;
using FSO.Files.Utils;
using FSO.LotView.Model;
using FSO.SimAntics.Model;

namespace FSO.SimAntics.Primitives
{

    public class VMDrop : VMPrimitiveHandler
    {
        private static LotTilePos[] Positions = { 
            new LotTilePos(0, -16, 0), //NORTH
            new LotTilePos(16, -16, 0), //NORTHWEST
            new LotTilePos(16, 0, 0), //WEST
            new LotTilePos(16, 16, 0), //SOUTHWEST
            new LotTilePos(0, 16, 0), //SOUTH
            new LotTilePos(-16, 16, 0), //SOUTHEAST
            new LotTilePos(-16, 0, 0), //EAST
            new LotTilePos(-16, -16, 0) //NORTHEAST
        };
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var obj = context.Caller;
            if (context.Caller.TotalSlots() == 0) return VMPrimitiveExitCode.GOTO_FALSE;
            var drop = context.Caller.GetSlot(0);
            if (drop == null) return VMPrimitiveExitCode.GOTO_FALSE;

            int intDir = (int)Math.Round(Math.Log((double)obj.Direction, 2));
            LotTilePos basePos = LotTilePos.FromBigTile(obj.Position.TileX, obj.Position.TileY, obj.Position.Level);

            for (int i = 0; i < Positions.Length; i++)
            {
                int j = (i % 2 == 1) ? ((Positions.Length - 1) - i / 2) : i / 2;

                var posChange = drop.MultitileGroup.ChangePosition(basePos + Positions[(j + intDir) % 8], obj.Direction, context.VM.Context, VMPlaceRequestFlags.Default);
                if (posChange.Status == VMPlacementError.Success)
                {
                    return VMPrimitiveExitCode.GOTO_TRUE;
                }
            }

            return VMPrimitiveExitCode.GOTO_FALSE;
        }
    }

    public class VMDropOperand : VMPrimitiveOperand
    {
        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes)
        {
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN))
            {
            }
        }

        public void Write(byte[] bytes) { }
        #endregion
    }
}
