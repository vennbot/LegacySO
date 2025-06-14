
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
using FSO.SimAntics.Model.TSOPlatform;
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.NetPlay.EODs.Model
{
    public class VMEODPluginThreadState : VMAsyncState
    {
        public short AvatarID;
        public short ObjectID;
        public bool Joinable;
        public bool Ended;
        public List<VMEODEvent> Events = new List<VMEODEvent>();

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(AvatarID);
            writer.Write(ObjectID);
            writer.Write(Joinable);
            writer.Write(Ended);
            writer.Write((byte)Events.Count);
            foreach (var evt in Events) evt.SerializeInto(writer);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            AvatarID = reader.ReadInt16();
            ObjectID = reader.ReadInt16();
            Joinable = reader.ReadBoolean();
            Ended = reader.ReadBoolean();
            Events = new List<VMEODEvent>();
            var totalEvt = reader.ReadByte();
            for (int i = 0; i < totalEvt; i++)
            {
                var evt = new VMEODEvent();
                evt.Deserialize(reader);
                Events.Add(evt);
            }
        }
    }
}
