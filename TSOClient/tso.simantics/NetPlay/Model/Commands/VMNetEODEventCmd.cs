
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
using FSO.SimAntics.NetPlay.EODs.Model;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetEODEventCmd : VMNetCommandBodyAbstract
    {
        public short ObjectID;
        public VMEODEvent Event;

        public override bool AcceptFromClient { get { return false; } }

        public override bool Execute(VM vm)
        {
            //notify thread of an EOD event.
            var obj = vm.GetObjectById(ObjectID);
            if (obj == null || obj.Thread == null || obj.Thread.EODConnection == null)
                return false; //rats.

            var state = obj.Thread.EODConnection;
            state.Events.Add(Event);

            if (Event.Code == -1) state.Ended = true;

            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return !FromNet;
        }

        #region VMSerializable Members
        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(ObjectID);
            Event.SerializeInto(writer);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            ObjectID = reader.ReadInt16();
            Event = new VMEODEvent();
            Event.Deserialize(reader);
        }
        #endregion
    }
}
