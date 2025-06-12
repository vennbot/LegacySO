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
using FSO.SimAntics.Engine;
using FSO.Vitaboy;
using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetDirectControlCommand : VMNetCommandBodyAbstract
    {
        public bool Partial;
        public VMDirectControlInput Input;

        public override bool Execute(VM vm, VMAvatar avatar)
        {
            if (avatar == null) return false;

            // Try and determine if the avatar is running a direct control frame.

            if (avatar.Thread.Stack.LastOrDefault() is VMDirectControlFrame dc)
            {
                // Forward the controls to it.

                dc.SendControls(Input);
            }
            else if (Input.LookDirectionReal != Vector3.Zero)
            {
                avatar.Avatar.HeadSeekTarget = Animator.CalculateHeadSeek(avatar.Avatar, Input.LookDirectionReal * 100, avatar.RadianDirection);
                avatar.Avatar.HeadSeek = avatar.Avatar.HeadSeekTarget;
                avatar.Avatar.HeadSeekWeight = 30f;
            }

            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return base.Verify(vm, caller) && (vm.Tuning.GetTuning("aprilfools", 0, 2023) ?? 0) != 0;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);

            writer.Write(Partial);

            if (Partial)
            {
                writer.Write(Input.LookDirectionReal.X);
                writer.Write(Input.LookDirectionReal.Y);
                writer.Write(Input.LookDirectionReal.Z);
            }
            else
            {
                writer.Write(Input.ID);
                writer.Write(Input.Direction);
                writer.Write(Input.InputIntensity);
                writer.Write(Input.LookDirectionInt);
                writer.Write(Input.LookDirectionReal.X);
                writer.Write(Input.LookDirectionReal.Y);
                writer.Write(Input.LookDirectionReal.Z);
                writer.Write(Input.Sprint);
            }
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);

            Partial = reader.ReadBoolean();

            if (Partial)
            {
                Input.LookDirectionReal = new Vector3(
                    reader.ReadSingle(),
                    reader.ReadSingle(),
                    reader.ReadSingle()
                    );
            }
            else
            {
                Input.ID = reader.ReadInt32();
                Input.Direction = reader.ReadInt16();
                Input.InputIntensity = reader.ReadInt32();
                Input.LookDirectionInt = reader.ReadInt16();
                Input.LookDirectionReal = new Vector3(
                    reader.ReadSingle(),
                    reader.ReadSingle(),
                    reader.ReadSingle()
                    );
                Input.Sprint = reader.ReadBoolean();
            }
        }

        #endregion
    }
}
