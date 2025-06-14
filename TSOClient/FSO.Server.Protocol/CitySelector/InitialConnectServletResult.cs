
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
using System;

namespace FSO.Server.Protocol.CitySelector
{
    public class InitialConnectServletResult : IXMLEntity
    {
        public InitialConnectServletResultType Status;
        public XMLErrorMessage Error;
        public UserAuthorized UserAuthorized;



        #region IXMLEntity Members

        public System.Xml.XmlElement Serialize(System.Xml.XmlDocument doc)
        {
            throw new NotImplementedException();
        }

        public void Parse(System.Xml.XmlElement element)
        {
            switch (element.Name)
            {
                case "Error-Message":
                    Status = InitialConnectServletResultType.Error;
                    Error = new XMLErrorMessage();
                    Error.Parse(element);
                    break;
                case "User-Authorized":
                    Status = InitialConnectServletResultType.Authorized;
                    UserAuthorized = new UserAuthorized();
                    UserAuthorized.Parse(element);
                    break;
                case "Patch-Result":
                    Status = InitialConnectServletResultType.Patch;
                    break;
            }
        }

        #endregion
    }

    public enum InitialConnectServletResultType
    {
        Authorized,
        Patch,
        Error
    }
}
