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
using System.Windows.Forms;
using FSO.Files.Formats.IFF;
using FSO.Content;
using FSO.Files.Formats.OTF;
using System.Xml;
using System.IO;

namespace FSO.IDE.ResourceBrowser.ResourceEditors
{
    public partial class OTFResourceControl : UserControl, IResourceControl
    {
        public OTFResourceControl()
        {
            InitializeComponent();
        }

        public void SetActiveResource(IffChunk chunk, GameIffResource res)
        {
            OTFFile tuning = null;
            if (res is GameObjectResource)
            {
                tuning = ((GameObjectResource)res).Tuning;
            }
            else if (res is GameGlobalResource)
            {
                tuning = ((GameGlobalResource)res).Tuning;
            }
            if (tuning == null)
            {
                XMLDisplay.Text = "No OTF is present for this iff.";
            }
            else
            {
                using (var stream = new StringWriter())
                {
                    var writer = new XmlTextWriter(stream);
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;

                    tuning.Document.Save(writer);

                    XMLDisplay.Text = stream.ToString();
                }
            }
        }

        public void SetActiveObject(GameObject obj)
        {

        }
        public void SetOBJDAttrs(OBJDSelector[] selectors)
        {

        }
    }
}
