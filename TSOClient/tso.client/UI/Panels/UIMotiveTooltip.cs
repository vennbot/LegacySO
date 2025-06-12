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
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;
using FSO.Common;
using FSO.Common.Rendering.Framework.Model;
using FSO.SimAntics;
using Microsoft.Xna.Framework;
using System;

namespace FSO.Client.UI.Panels
{
    internal class UIMotiveTooltip : UICachedContainer
    {
        private UIImage Background;
        private UILabel Title;
        private UIMotiveDisplay Motives;

        private VMAvatar ActiveAvatar;
        private bool Show;

        public UIMotiveTooltip()
        {
            var ui = Content.Content.Get().CustomUI;
            var gd = GameFacade.GraphicsDevice;

            Background = new UIImage(ui.Get("neighp_tabtop_bg.png").Get(gd)).With9Slice(30, 30, 30, 30);

            Title = new UILabel()
            { 
                Position = new Vector2(16, 8),
                Size = new Vector2(170, 1),
                Alignment = TextAlignment.Center | TextAlignment.Top,
                Caption = GameFacade.Strings.GetString("174", "3"),
                CaptionStyle = TextStyle.DefaultLabel.Clone()
            };

            Motives = new UIMotiveDisplay() { Position = new Vector2(1, 29), DynamicMode = true };

            Title.CaptionStyle.Shadow = true;

            Background.SetSize(202, 124);

            Add(Background);
            Add(Title);
            Add(Motives);

            Size = new Vector2(202, 124);

            Opacity = 0;
            Visible = false;
        }

        public override void Update(UpdateState state)
        {
            var fadeSpeed = 5f / FSOEnvironment.RefreshRate;
            if (Show)
            {
                Opacity = Math.Min(1, Opacity + fadeSpeed);
            }
            else
            {
                Opacity = Math.Max(0, Opacity - fadeSpeed);
            }

            Visible = Opacity > 0;

            if (Visible && ActiveAvatar != null)
            {
                Motives.UpdateMotives(ActiveAvatar, null);
                Invalidate();
            }

            base.Update(state);
        }

        public void SetActiveAvatar(VMAvatar avatar)
        {
            ActiveAvatar = avatar;
            Show = avatar != null;

            if (Show)
            {
                Motives.UpdateMotives(ActiveAvatar, null, true);
                Invalidate();
            }
        }
    }
}
