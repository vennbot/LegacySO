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
using System;
using System.IO;

namespace FSO.Files.Formats.IFF.Chunks
{
    public class CARR : IffChunk
    {
        public string Name;
        public JobLevel[] JobLevels;

        public override void Read(IffFile iff, Stream stream)
        {
            using (var io = IoBuffer.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                io.ReadUInt32(); //pad
                var version = io.ReadUInt32();

                var MjbO = io.ReadUInt32();

                var compressionCode = io.ReadByte();
                if (compressionCode != 1) throw new Exception("hey what!!");

                Name = io.ReadNullTerminatedString();
                if (Name.Length % 2 == 1) io.ReadByte();
                var iop = new IffFieldEncode(io);


                var numLevels = iop.ReadInt32();

                JobLevels = new JobLevel[numLevels];
                for (int i=0; i<numLevels; i++)
                {
                    JobLevels[i] = new JobLevel(iop);
                }
            }
        }

        public int GetJobData(int level, int data)
        {
            var entry = JobLevels[level];
            switch (data)
            {
                case 0: //number of levels
                    return JobLevels.Length;
                case 1: //salary
                    return entry.Salary;
                case 12: //start hour
                    return entry.StartTime;
                case 13:
                    return entry.EndTime;
                case 21:
                    return entry.CarType;
                case 22:
                    return 0;
                default:
                    if (data < 12)
                        return entry.MinRequired[data-2];
                    else
                        return entry.MotiveDelta[data-14];
            }
        }
    }

    public class JobLevel
    {
        public int[] MinRequired = new int[10]; //friends, then skills.
        public int[] MotiveDelta = new int[7];
        public int Salary;
        public int StartTime;
        public int EndTime;
        public int CarType;

        public string JobName;
        public string MaleUniformMesh;
        public string FemaleUniformMesh;
        public string UniformSkin;
        public string unknown;

        public JobLevel(IffFieldEncode iop)
        {
            for (int i=0; i<MinRequired.Length; i++)
                MinRequired[i] = iop.ReadInt32();
            for (int i = 0; i < MotiveDelta.Length; i++)
                MotiveDelta[i] = iop.ReadInt32();
            Salary = iop.ReadInt32();
            StartTime = iop.ReadInt32();
            EndTime = iop.ReadInt32();
            CarType = iop.ReadInt32();

            JobName = iop.ReadString(false);
            MaleUniformMesh = iop.ReadString(false);
            FemaleUniformMesh = iop.ReadString(false);
            UniformSkin = iop.ReadString(false);
            unknown = iop.ReadString(true);
        }
    }
}
