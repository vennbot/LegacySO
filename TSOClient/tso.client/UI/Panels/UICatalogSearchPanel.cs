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
using FSO.Common.Rendering.Framework.Model;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FSO.Client.UI.Panels
{
    public class UICatalogSearchPanel: UIContainer
    {
        public delegate void SearchUpdateHandler(string term);

        private const float HiddenYOffset = 10;
        private const float ShownYOffset = -30;

        private UIImage Background;
        private UITextBox SearchBox;
        private UIAbstractCatalogPanel CatalogParent;

        private bool Shown;
        private bool ShouldFocus;

        private float _YOffset;

        public float YOffset
        {
            get
            {
                return _YOffset;
            }
            set
            {
                Y = CatalogParent.Y + value;
                _YOffset = value;
            }
        }

        private float _XOffset;

        public float XOffset
        {
            get
            {
                return _XOffset;
            }
            set
            {
                X = CatalogParent.X + value;
                _XOffset = value;
            }
        }

        public event SearchUpdateHandler OnUpdate;

        public UICatalogSearchPanel(UIAbstractCatalogPanel parent)
        {
            CatalogParent = parent;

            var ui = Content.Content.Get().CustomUI;
            var gd = GameFacade.GraphicsDevice;

            Background = new UIImage(ui.Get("cat_search_bg.png").Get(gd));
            this.Add(Background);

            SearchBox = new UITextBox()
            {
                X = 5,
                Y = 4
            };
            SearchBox.SetBackgroundTexture(null, 0, 0, 0, 0);

            SearchBox.SetSize(250, 25);
            SearchBox.OnChange += Changed;
            this.Add(SearchBox);
        }

        private void Changed(UIElement element)
        {
            OnUpdate(SearchBox.CurrentText);
        }

        public bool Toggle()
        {
            if (Shown)
            {
                Hide();
            }
            else
            {
                Show();
            }

            return Shown;
        }

        public void Show()
        {
            Shown = true;
            GameFacade.Screens.Tween.To(this, 0.33f, new Dictionary<string, float>() { { "YOffset", ShownYOffset } }, TweenQuad.EaseOut);

            ShouldFocus = true;
        }

        public void Hide()
        {
            Shown = false;
            GameFacade.Screens.Tween.To(this, 0.33f, new Dictionary<string, float>() { { "YOffset", HiddenYOffset } }, TweenQuad.EaseOut);

            if (SearchBox.CurrentText != "")
            {
                SearchBox.CurrentText = "";
                OnUpdate("");
            }
        }

        public void SetParent(UIContainer container)
        {
            if (container != null)
            {
                container.AddAt(0, this);
                YOffset = HiddenYOffset;
                XOffset = XOffset;
            }
        }

        public override void Update(UpdateState state)
        {
            if (ShouldFocus)
            {
                state.InputManager.SetFocus(SearchBox);

                ShouldFocus = false;
            }

            bool isFocus = state.InputManager.GetFocus() == SearchBox;

            if (isFocus && state.KeyboardState.IsKeyDown(Keys.Escape))
            {
                Hide();
            }

            if (!Shown && isFocus)
            {
                state.InputManager.SetFocus(null);
            }

            base.Update(state);
        }
    }
}
