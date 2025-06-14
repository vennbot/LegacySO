
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
    public class UserAuthorized : IXMLEntity
    {
        public string FSOVersion;
        public string FSOBranch;
        public string FSOUpdateUrl;
        public string FSOCDNUrl;

        public System.Xml.XmlElement Serialize(System.Xml.XmlDocument doc)
        {
            var element = doc.CreateElement("User-Authorized");
            element.AppendTextNode("FSO-Version", FSOVersion);
            element.AppendTextNode("FSO-Branch", FSOBranch);
            element.AppendTextNode("FSO-UpdateUrl", FSOUpdateUrl);
            element.AppendTextNode("FSO-CDNUrl", FSOCDNUrl);
            return element;
        }

        public void Parse(System.Xml.XmlElement element)
        {
            this.FSOVersion = element.ReadTextNode("FSO-Version");
            this.FSOBranch = element.ReadTextNode("FSO-Branch");
            this.FSOUpdateUrl = element.ReadTextNode("FSO-UpdateUrl");
            this.FSOCDNUrl = element.ReadTextNode("FSO-CDNUrl");
        }
    }
}
