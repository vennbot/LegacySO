
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
using System;
using FSO.Common.Utils;

namespace FSO.Server.Protocol.CitySelector
{
    public class ShardStatusItem : IXMLEntity
    {
        public string Name;
        public int Rank;
        public string Map;
        public ShardStatus Status;
        public int Id;
        public string PublicHost;
        public string InternalHost;
        public string VersionName;
        public string VersionNumber;
        public int? UpdateID;

        public ShardStatusItem()
        {
        }

        public System.Xml.XmlElement Serialize(System.Xml.XmlDocument doc)
        {
            var result = doc.CreateElement("Shard-Status");
            result.AppendTextNode("Location", "public");
            result.AppendTextNode("Name", Name);
            result.AppendTextNode("Rank", Rank.ToString());
            result.AppendTextNode("Map", Map);
            result.AppendTextNode("Status", Status.ToString());
            result.AppendTextNode("Id", Id.ToString());
            return result;
        }

        public void Parse(System.Xml.XmlElement element)
        {
            this.Name = element.ReadTextNode("Name");
            this.Rank = int.Parse(element.ReadTextNode("Rank"));
            this.Map = element.ReadTextNode("Map");
            this.Status = (ShardStatus)Enum.Parse(typeof(ShardStatus), element.ReadTextNode("Status"));
            this.Id = int.Parse(element.ReadTextNode("Id"));
        }
    }
}
