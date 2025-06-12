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
using FSO.Client.Model;
using FSO.Client.Regulators;
using FSO.Common.Domain.Shards;
using FSO.Server.Clients;
using FSO.Server.Protocol.CitySelector;
using System.Linq;

namespace FSO.Client.Network
{
    public class Network
    {
        private CityConnectionRegulator CityRegulator;
        private LotConnectionRegulator LotRegulator;
        private LoginRegulator LoginRegulator;
        private IShardsDomain Shards;

        public Network(LoginRegulator loginReg, CityConnectionRegulator cityReg, LotConnectionRegulator lotReg, IShardsDomain shards)
        {
            this.Shards = shards;
            this.CityRegulator = cityReg;
            this.LoginRegulator = loginReg;
            this.LotRegulator = lotReg;
        }

        public AriesClient CityClient
        {
            get
            {
                return CityRegulator.Client;
            }
        }

        public AriesClient LotClient
        {
            get
            {
                return LotRegulator.Client;   
            }
        }

        public UserReference MyCharacterRef
        {
            get
            {
                return UserReference.Of(Common.Enum.UserReferenceType.AVATAR, MyCharacter);
            }
        }

        public uint MyCharacter
        {
            get
            {
                return uint.Parse(CityRegulator.CurrentShard.AvatarID);
            }
        }

        public ShardStatusItem MyShard
        {
            get
            {
                return Shards.All.First(x => x.Name == CityRegulator.CurrentShard.ShardName);
            }
        }
    }
}
