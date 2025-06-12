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
using FSO.Content;
using FSO.Files.Formats.IFF;
using FSO.Files.Formats.IFF.Chunks;
using System;
using System.Windows.Forms;

namespace FSO.IDE.ResourceBrowser
{
    public partial class SPR2SelectorDialog : Form
    {
        public ushort ChosenID;

        public SPR2SelectorDialog()
        {
            InitializeComponent();
        }

        public SPR2SelectorDialog(GameIffResource iff, GameObject srcObj) : this()
        {
            iffRes.Init(
                new Type[] { typeof(SPR2) },
                new string[] { "Sprites" },
                new OBJDSelector[][]
                {
                    new OBJDSelector[]
                    {
                        new OBJDSelector("Chosen Sprite", null, new OBJDSelector.OBJDSelectorCallback((IffChunk chunk) => {
                            var spr = (SPR2)chunk;
                            if (spr != null) {
                                ChosenID = spr.ChunkID;
                                DialogResult = DialogResult.OK;
                            }
                            Close();
                        }))
                    }
                }
                );
            iffRes.ChangeIffSource(iff);
            iffRes.ChangeActiveObject(srcObj);

            iffRes.SetAlphaOrder(false);
        }
    }
}
