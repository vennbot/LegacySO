
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
using FSO.Client.UI.Framework;
using FSO.Client.UI.Controls;
using Microsoft.Xna.Framework;

namespace FSO.Client.UI.Panels
{
    //in matchmaker displays title of city. in lot displays lot name.

    public class UIGameTitle : UICachedContainer
    {
        public UIImage Background;
        public UILabel Label;
        public UIButton CancelButton;

        private string Title;
        private Tuple<string, Action> OverrideMode;

        public UIGameTitle()
        {
            Background = new UIImage(GetTexture((ulong)0x000001A700000002));
            Background.With9Slice(40, 40, 0, 0);
            this.AddAt(0, Background);
            Background.BlockInput();

            Label = new UILabel();
            Label.CaptionStyle = TextStyle.DefaultLabel.Clone();
            Label.CaptionStyle.Size = 11;
            Label.Alignment = TextAlignment.Middle;
            this.Add(Label);

            var ui = Content.Content.Get().CustomUI;
            var btnTex = ui.Get("chat_cat.png").Get(GameFacade.GraphicsDevice);

            var btnCaption = TextStyle.DefaultLabel.Clone();
            btnCaption.Size = 8;
            btnCaption.Shadow = true;

            CancelButton = new UIButton(btnTex);
            CancelButton.Caption = GameFacade.Strings.GetString("f115", "48");
            CancelButton.CaptionStyle = btnCaption;
            CancelButton.OnButtonClick += CancelOverride;
            CancelButton.Width = 64;
            CancelButton.Y = 2;
            Add(CancelButton);

            SetTitle("Not Blazing Falls");
        }

        private void CancelOverride(UIElement button)
        {
            OverrideMode?.Item2?.Invoke();
        }

        public void SetOverrideMode(string title, Action callback)
        {
            Label.Caption = title;

            var style = Label.CaptionStyle;

            var twidth = style.MeasureString(title).X;
            var ScreenWidth = GlobalSettings.Default.GraphicsWidth / 2;

            var width = twidth + 72;

            X = ScreenWidth - (width / 2 + 40);
            Background.X = 0;
            Background.SetSize(width + 80, 24);
            Size = new Vector2(width + 80, 24);

            Label.X = 40;
            Label.Size = new Vector2(width, 20);

            //cancel button

            CancelButton.Visible = true;
            CancelButton.X = twidth + 48;
            CancelButton.Y = 2;
            CancelButton.Width = 64;

            OverrideMode = new Tuple<string, Action>(title, callback);
        }

        public void ClearOverrideMode()
        {
            OverrideMode = null;
            SetNormalTitle(Title);
        }

        public void SetTitle(string title)
        {
            Title = title;
            if (OverrideMode == null) SetNormalTitle(Title);
        }

        private void SetNormalTitle(string title)
        {
            Label.Caption = title;

            var style = Label.CaptionStyle;

            var width = style.MeasureString(title).X;
            var ScreenWidth = GlobalSettings.Default.GraphicsWidth/2;

            X = ScreenWidth - (width / 2 + 40);
            Background.X = 0;
            Background.SetSize(width + 80, 24);
            Size = new Vector2(width + 80, 24);

            Label.X = 40;
            Label.Size = new Vector2(width, 20);

            CancelButton.Visible = false;
        }
    }
}
