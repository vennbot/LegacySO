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
using System.IO;

namespace FSO.Vitaboy
{
    public class BCF
    {
        public Skeleton[] Skeletons;
        public Appearance[] Appearances;
        public Animation[] Animations;

        public BCF(Skeleton[] skeletons, Appearance[] appearances, Animation[] animations)
        {
            Skeletons = skeletons;
            Appearances = appearances;
            Animations = animations;
        }

        public BCF(Stream stream, bool cmx)
        {
            using (var io = (cmx) ? new BCFReadString(stream, true) : (BCFReadProxy)IoBuffer.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                Skeletons = new Skeleton[io.ReadInt32()];
                for (int i = 0; i < Skeletons.Length; i++)
                {
                    Skeletons[i] = new Skeleton();
                    Skeletons[i].Read(io, true);
                    Skeletons[i].ParentBCF = this;
                }
                Appearances = new Appearance[io.ReadInt32()];
                for (int i = 0; i < Appearances.Length; i++)
                {
                    Appearances[i] = new Appearance();
                    Appearances[i].ReadBCF(io);
                    Appearances[i].ParentBCF = this;
                }
                Animations = new Animation[io.ReadInt32()];
                for (int i = 0; i < Animations.Length; i++)
                {
                    Animations[i] = new Animation();
                    Animations[i].Read(io, true);
                    Animations[i].ParentBCF = this;
                }
            }
        }

        public void Write(Stream stream, bool cmx)
        {
            using (var io = (cmx) ? new BCFWriteString(stream, true) : (BCFWriteProxy)IoWriter.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                io.WriteInt32(Skeletons.Length);
                for (int i = 0; i < Skeletons.Length; i++)
                {
                    Skeletons[i].Write(io, true);
                }
                io.WriteInt32(Appearances.Length);
                for (int i = 0; i < Appearances.Length; i++)
                {
                    Appearances[i].WriteBCF(io);
                }
                io.WriteInt32(Animations.Length);
                for (int i = 0; i < Animations.Length; i++)
                {
                    Animations[i].Write(io, true);
                }
            }
        }
    }
}
