
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
using FSO.Files.Formats.IFF.Chunks;
using FSO.LotView.Model;
using System.IO;

namespace FSO.SimAntics.Primitives
{
    public class VMGotoRelativePosition : VMPrimitiveHandler
    {

        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMGotoRelativePositionOperand)args;

            if (context.Thread.IsCheck) return VMPrimitiveExitCode.GOTO_FALSE;

            var obj = context.StackObject;
            if (obj == null) return VMPrimitiveExitCode.GOTO_FALSE;

            if (obj.Position == LotTilePos.OUT_OF_WORLD) return VMPrimitiveExitCode.GOTO_FALSE;

            var slot = new SLOTItem { Type = 3, Standing = 1 };

            if (operand.Location != VMGotoRelativeLocation.OnTopOf) { //default slot is on top of

                if (operand.Location == VMGotoRelativeLocation.AnywhereNear) {
                    slot.MinProximity = 16;
                    slot.MaxProximity = 32; //diamond shaped
                    slot.OptimalProximity = 16;
                    slot.Rsflags |= (SLOTFlags)255;
                }
                else
                {
                    slot.MinProximity = 16;
                    slot.MaxProximity = 24;
                    slot.Rsflags |= (SLOTFlags)(1 << (((int)operand.Location) % 8));
                }
            }

            if (operand.Direction == VMGotoRelativeDirection.AnyDirection) slot.Facing = SLOTFacing.FaceAnywhere; //TODO: verify. not sure where this came from?
            else slot.Facing = (SLOTFacing)operand.Direction;

            var pathFinder = context.Thread.PushNewRoutingFrame(context, !operand.NoFailureTrees);
            pathFinder.InitRoutes(slot, context.StackObject);

            return VMPrimitiveExitCode.CONTINUE;
        }
    }

    public class VMGotoRelativePositionOperand : VMPrimitiveOperand
    {
        /** How long to meander around objects **/
        public ushort OldTrapCount { get; set; }
        public VMGotoRelativeLocation Location { get; set; }
        public VMGotoRelativeDirection Direction { get; set; }
        public ushort RouteCount { get; set; }
        public VMGotoRelativeFlags Flags { get; set; }

        public bool NoFailureTrees
        {
            get
            {
                return (Flags & VMGotoRelativeFlags.NoFailureTrees) > 0;
            }
            set
            {
                Flags = (Flags & ~VMGotoRelativeFlags.NoFailureTrees);
                if (value) Flags |= VMGotoRelativeFlags.NoFailureTrees;
            }
        }

        public bool AllowDiffAlt
        {
            get
            {
                return (Flags & VMGotoRelativeFlags.AllowDiffAlt) > 0;
            }
            set
            {
                Flags = (Flags & ~VMGotoRelativeFlags.AllowDiffAlt);
                if (value) Flags |= VMGotoRelativeFlags.AllowDiffAlt;
            }
        }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes){
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN)){
                OldTrapCount = io.ReadUInt16();
                Location = (VMGotoRelativeLocation)((sbyte)io.ReadByte());
                Direction = (VMGotoRelativeDirection)((sbyte)io.ReadByte());
                RouteCount = io.ReadUInt16();
                Flags = (VMGotoRelativeFlags)io.ReadByte();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(OldTrapCount);
                io.Write((byte)Location);
                io.Write((byte)Direction);
                io.Write(RouteCount);
                io.Write((byte)Flags);
            }
        }
        #endregion
    }

    public enum VMGotoRelativeDirection
    {
        Facing = -2,
        AnyDirection = -1,
        SameDirection = 0,
        FortyFiveDegreesRightOfSameDirection = 1,
        NinetyDegreesRightOfSameDirection = 2,
        FortyFiveDegreesLeftOfOpposingDirection = 3,
        OpposingDirection = 4,
        FortyFiveDegreesRightOfOpposingDirection = 5,
        NinetyDegreesRightOfOpposingDirection = 6,
        FortyFiveDegreesLeftOfSameDirection = 7
    }

    public enum VMGotoRelativeLocation {
        OnTopOf = -2,
        AnywhereNear = -1,
        InFrontOf = 0,
        FrontAndToRightOf = 1,
        ToTheRightOf = 2,
        BehindAndToRightOf = 3,
        Behind = 4,
        BehindAndToTheLeftOf = 5,
        ToTheLeftOf = 6,
        InFrontAndToTheLeftOf = 7
    }

    [Flags]
    public enum VMGotoRelativeFlags : byte
    {
        AllowDiffAlt = 0x1,
        NoFailureTrees = 0x2
    }
}
