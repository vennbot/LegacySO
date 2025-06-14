
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
using FSO.Client.UI.Framework;
using Microsoft.Xna.Framework;
using FSO.Client.UI.Model;
using FSO.Client.GameContent;

namespace FSO.Client.UI.Controls
{
    public class UITextBox : UITextEdit
    {
        public static ITextureRef StandardBackground;
        public bool HasText => !string.IsNullOrWhiteSpace(CurrentText);
        static UITextBox()
        {
            var tex = UIElement.GetTexture((ulong)FileIDs.UIFileIDs.dialog_textboxbackground);
            if (tex.Width == 1) return;
            StandardBackground = new SlicedTextureRef(
                UIElement.GetTexture((ulong)FileIDs.UIFileIDs.dialog_textboxbackground),
                new Microsoft.Xna.Framework.Rectangle(13, 13, 13, 13)
            );
        }

        public UITextBox() : base()
        {
            MaxLines = 1;
            BackgroundTextureReference = UITextBox.StandardBackground;
            TextMargin = new Rectangle(8, 2, 8, 3);
        }

        public void Clear()
        {
            SelectionEnd = -1;
            SelectionStart = -1;
            m_SBuilder.Clear();
        }
       
    }
}
