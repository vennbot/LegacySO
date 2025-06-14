
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
using Microsoft.Xna.Framework;
using FSO.Common.Rendering.Framework.IO;
using FSO.Common.Rendering.Framework.Model;

namespace FSO.Client.UI.Controls
{
    public class UIClickableLabel : UILabel
    {
        private UIMouseEventRef ClickHandler;
        public event ButtonClickDelegate OnButtonClick;
        public event UIMouseEvent OnMouseEvtExt;

        public UIClickableLabel()
        {
            ClickHandler =
                ListenForMouse(new Rectangle(0, 0, 10, 10), new UIMouseEvent(OnMouseEvent));
        }


        public override Vector2 Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
                ClickHandler.Region = new Rectangle(0, 0, (int)value.X, (int)value.Y);
            }
        }



        //private bool m_isOver;
        //todo - use m_isOver to show diff colour for hovering over labels. 
        private bool m_isDown;

        private void OnMouseEvent(UIMouseEventType type, UpdateState state)
        {
            switch (type)
            {
                case UIMouseEventType.MouseOver:
                    //m_isOver = true;
                    break;

                case UIMouseEventType.MouseOut:
                    //m_isOver = false;
                    break;

                case UIMouseEventType.MouseDown:
                    m_isDown = true;
                    break;

                case UIMouseEventType.MouseUp:
                    if (m_isDown)
                    {
                        if (OnButtonClick != null)
                        {
                            OnButtonClick(this);
                            //GameFacade.SoundManager.PlayUISound(1);
                        }
                    }
                    m_isDown = false;
                    break;
            }
            OnMouseEvtExt?.Invoke(type, state);
        }

    }
}
