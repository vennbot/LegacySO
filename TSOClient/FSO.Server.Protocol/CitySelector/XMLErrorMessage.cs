
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
    public class XMLErrorMessage : IXMLEntity
    {
        public String Code;
	    public String Message;
    	
	    public XMLErrorMessage(){
	    }

        public XMLErrorMessage(String code, String message)
        {
            this.Code = code;
            this.Message = message;
	    }

        #region IXMLPrinter Members

        public System.Xml.XmlElement Serialize(System.Xml.XmlDocument doc)
        {
            var element = doc.CreateElement("Error-Message");
            element.AppendTextNode("Error-Number", Code);
            element.AppendTextNode("Error", Message);
            return element;
        }

        public void Parse(System.Xml.XmlElement element)
        {
            this.Code = element.ReadTextNode("Error-Number");
            this.Message = element.ReadTextNode("Error");
        }

        #endregion
    }
}
