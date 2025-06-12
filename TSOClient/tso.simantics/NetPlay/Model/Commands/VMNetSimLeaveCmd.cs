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
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    /// <summary>
    /// Causes a sim to begin being deleted. Can be user initiated, but they will be disconnected when their sim is fully gone.
    /// </summary>
    public class VMNetSimLeaveCmd : VMNetCommandBodyAbstract
    {
        public override bool Execute(VM vm, VMAvatar sim)
        {
            if (sim != null && !sim.Dead)
            {
                // the user has left the lot with their sim still on it...
                // force leave lot. generate an action with incredibly high priority and cancel current

                sim.UserLeaveLot();
            }
            return true;
        }

        #region VMSerializable Members
        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
        }
        #endregion
    }
}
