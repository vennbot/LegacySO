
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
using System.Xml;

namespace FSO.Common.Utils
{
    public interface IXMLEntity
    {
        XmlElement Serialize(XmlDocument doc);
        void Parse(XmlElement element);
    }

    public static class XMLUtils
    {
        public static T Parse<T>(string data) where T : IXMLEntity
        {
            var doc = new XmlDocument();
            doc.LoadXml(data);

            T result = (T)Activator.CreateInstance(typeof(T));
            result.Parse((XmlElement)doc.FirstChild);
            return result;
        }

        public static void AppendTextNode(this XmlElement e, string nodeName, string value)
        {
            var node = e.OwnerDocument.CreateElement(nodeName);
            node.AppendChild(e.OwnerDocument.CreateTextNode(value));
            e.AppendChild(node);
        }

        public static string ReadTextNode(this XmlElement e, string nodeName)
        {
            foreach (XmlElement child in e.ChildNodes)
            {
                if (child.Name == nodeName && child.FirstChild != null)
                {
                    return child.FirstChild?.Value;
                }
            }
            return null;
        }
    }
}
