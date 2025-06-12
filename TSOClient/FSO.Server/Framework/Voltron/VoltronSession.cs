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
using FSO.Common.Security;
using FSO.Server.Framework.Aries;
using FSO.Server.Protocol.Voltron.Packets;
using Mina.Core.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace FSO.Server.Framework.Voltron
{
    public class VoltronSession : AriesSession, IVoltronSession
    {
        public uint UserId { get; set; }
        public uint AvatarId { get; set; }
        public int AvatarClaimId { get; set; }

        public bool IsAnonymous
        {
            get
            {
                return AvatarId == 0;
            }
        }

        public string IpAddress
        {
            get
            {
                return IoSession.RemoteEndPoint.ToString();
            }
        }

        public VoltronSession(IoSession ioSession) : base(ioSession){
        }

        public override void Close()
        {
            Write(new ServerByePDU() { }); //try and close the connection safely
            base.Close();
        }


        public void DemandAvatar(uint id, AvatarPermissions permission)
        {
            if(AvatarId != id){
                throw new SecurityException("Permission " + permission + " denied for avatar " + AvatarId);
            }
        }

        public void DemandAvatars(IEnumerable<uint> ids, AvatarPermissions permission)
        {
            if (!ids.Contains(AvatarId))
            {
                throw new SecurityException("Permission " + permission + " denied for avatar " + AvatarId);
            }
        }

        public void DemandInternalSystem()
        {
            throw new SecurityException("Voltron sessions are not trusted internal systems");
        }
    }
}
