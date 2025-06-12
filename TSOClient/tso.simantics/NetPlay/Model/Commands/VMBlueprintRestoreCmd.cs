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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FSO.LotView.Model;
using FSO.SimAntics.Utils;
using FSO.Files.Formats.IFF;
using FSO.Files.Formats.IFF.Chunks;
using FSO.SimAntics.Marshals;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMBlueprintRestoreCmd : VMNetCommandBodyAbstract
    {
        public byte[] XMLData;
        public short JobLevel = -1;
        public int FloorClipX;
        public int FloorClipY;
        public int FloorClipWidth;
        public int FloorClipHeight;
        public int OffsetX;
        public int OffsetY;
        public int TargetSize;
        public bool IffData;

        public override bool AcceptFromClient { get { return false; } }

        public override bool Execute(VM vm)
        {
            //the client should ignore these. Can be sent before state sync when joining job lots (by accident)
            if (!vm.BlueprintRestore) return true;

            vm.SetGlobalValue(11, JobLevel); //set job level beforehand 

            if (IffData)
            {
                var iff = new IffFile();

                using (var stream = new MemoryStream(XMLData))
                {
                    iff.Read(stream);
                }

                var fsov = iff.List<FSOV>()?.FirstOrDefault();

                if (fsov != null)
                {
                    var marshal = new VMMarshal();
                    using (var read = new BinaryReader(new MemoryStream(fsov.Data)))
                        marshal.Deserialize(read);
                    vm.Load(marshal);
                }
                else
                {
                    //var activator = new VMTS1Activator(vm, vm.Context.World, JobLevel);
                    var activator = new VMTS1ActivatorNew(vm, JobLevel);
                    activator.LoadFromIff(iff);
                }

                var entClone = new List<VMEntity>(vm.Entities);

                foreach (var nobj in entClone)
                {
                    nobj.ExecuteEntryPoint(2, vm.Context, true);
                }

                vm.TS1State.VerifyFamily(vm);
            }
            else
            {
                XmlHouseData lotInfo;
                using (var stream = new MemoryStream(XMLData))
                {
                    lotInfo = XmlHouseData.Parse(stream);
                }

                var activator = new VMWorldActivator(vm, vm.Context.World);
                activator.FloorClip = new Microsoft.Xna.Framework.Rectangle(FloorClipX, FloorClipY, FloorClipWidth, FloorClipHeight);
                activator.Offset = new Microsoft.Xna.Framework.Point(OffsetX, OffsetY);
                activator.TargetSize = TargetSize;
                activator.LoadFromXML(lotInfo);
            }

            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return !FromNet;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            if (XMLData == null) writer.Write(0);
            else
            {
                writer.Write(XMLData.Length);
                writer.Write(XMLData);
                writer.Write(JobLevel);

                writer.Write(FloorClipX);
                writer.Write(FloorClipY);
                writer.Write(FloorClipWidth);
                writer.Write(FloorClipHeight);
                writer.Write(OffsetX);
                writer.Write(OffsetY);
                writer.Write(TargetSize);
            }
        }

        public override void Deserialize(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            XMLData = reader.ReadBytes(length);
            JobLevel = reader.ReadInt16();

            FloorClipX = reader.ReadInt32();
            FloorClipY = reader.ReadInt32();
            FloorClipWidth = reader.ReadInt32();
            FloorClipHeight = reader.ReadInt32();
            OffsetX = reader.ReadInt32();
            OffsetY = reader.ReadInt32();
            TargetSize = reader.ReadInt32();
        }

        #endregion
    }
}
