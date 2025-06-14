
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
using System.Collections.Generic;
using System.Xml;

namespace FSO.Common.Utils
{
    public class XMLList<T> : List<T>, IXMLEntity where T : IXMLEntity
    {
        private string NodeName;

        public XMLList(string nodeName)
        {
            this.NodeName = nodeName;
        }

        public XMLList()
        {
            this.NodeName = "Unknown";
        }

        #region IXMLPrinter Members

        public System.Xml.XmlElement Serialize(System.Xml.XmlDocument doc)
        {
            var element = doc.CreateElement(NodeName);
            foreach (var child in this)
            {
                element.AppendChild(child.Serialize(doc));
            }
            return element;
        }

        public void Parse(System.Xml.XmlElement element)
        {
            var type = typeof(T);

            foreach (XmlElement child in element.ChildNodes)
            {
                var instance = (T)Activator.CreateInstance(type);
                instance.Parse(child);
                this.Add(instance);
            }
        }

        #endregion
    }
}
