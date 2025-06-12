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
using FSO.Common.Utils;

namespace FSO.Server.Protocol.CitySelector
{
    public class ShardSelectorServletResponse : IXMLEntity
    {
        public string Address;
        public string Ticket;
        public string ConnectionID;
        public uint PlayerID;
        public string AvatarID;

        public bool PreAlpha = false;

        #region IXMLPrinter Members

        public System.Xml.XmlElement Serialize(System.Xml.XmlDocument doc)
        {
            var result = doc.CreateElement("Shard-Selection");
            result.AppendTextNode("Connection-Address", Address);
            result.AppendTextNode("Authorization-Ticket", Ticket);
            result.AppendTextNode("PlayerID", PlayerID.ToString());

            if (PreAlpha == false)
            {
                result.AppendTextNode("ConnectionID", ConnectionID);
                result.AppendTextNode("EntitlementLevel", "");
            }
            result.AppendTextNode("AvatarID", AvatarID); //legacyso now uses this

            return result;
        }

        public void Parse(System.Xml.XmlElement element)
        {
            this.Address = element.ReadTextNode("Connection-Address");
            this.Ticket = element.ReadTextNode("Authorization-Ticket");
            this.PlayerID = uint.Parse(element.ReadTextNode("PlayerID"));

            this.AvatarID = element.ReadTextNode("AvatarID");
        }

        #endregion
    }
}
