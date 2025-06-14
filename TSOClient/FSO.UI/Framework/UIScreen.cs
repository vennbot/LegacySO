
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FSO.Common;

namespace FSO.Client.UI.Framework
{
    public class UIScreen : UIContainer
    {
        public UIScreen() : base()
        {
            ScaleX = ScaleY = FSOEnvironment.DPIScaleFactor;
        }

        public virtual void OnShow()
        {
        }

        public virtual void OnHide()
        {

        }

        public static UIScreen Current
        {
            get
            {
                return GameFacade.Screens.CurrentUIScreen;
            }
        }

        public static UIAlert GlobalShowAlert(UIAlertOptions options, bool modal)
        {
            var alert = new UIAlert(options);
            GlobalShowDialog(alert, modal);
            alert.CenterAround(UIScreen.Current, -(int)UIScreen.Current.X * 2, -(int)UIScreen.Current.Y * 2);
            return alert;
        }

        /// <summary>
        /// Adds a popup dialog
        /// </summary>
        /// <param name="dialog"></param>
        public static void GlobalShowDialog(UIElement dialog, bool modal)
        {
            GlobalShowDialog(new DialogReference
            {
                Dialog = dialog,
                Modal = modal
            });
        }

        public static void GlobalShowDialog(DialogReference dialog)
        {
            GameFacade.Screens.AddDialog(dialog);

            if (dialog.Dialog is UIDialog)
            {
                ((UIDialog)dialog.Dialog).CenterAround(UIScreen.Current, -(int)UIScreen.Current.X * 2, -(int)UIScreen.Current.Y * 2);
            }
        }

        /// <summary>
        /// Adds a popup dialog
        /// </summary>
        /// <param name="dialog"></param>
        public static void ShowDialog(UIElement dialog, bool modal)
        {
            GameFacade.Screens.AddDialog(new DialogReference
            {
                Dialog = dialog,
                Modal = modal
            });

            if (dialog is UIDialog)
            {
                ((UIDialog)dialog).CenterAround(UIScreen.Current, -(int)UIScreen.Current.X*2, -(int)UIScreen.Current.Y * 2);
            }
        }

        public virtual void DeviceReset(GraphicsDevice Device) { }

        /// <summary>
        /// Removes a previously shown dialog
        /// </summary>
        /// <param name="dialog"></param>
        public static void RemoveDialog(UIElement dialog)
        {
            GameFacade.Screens.RemoveDialog(dialog);
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(0, 0, ScreenWidth, ScreenHeight);
        }

        public virtual bool CloseAttempt()
        {
            return true;
        }

        public int ScreenWidth
        {
            get
            {
                return GlobalSettings.Default.GraphicsWidth;
            }
        }

        public int ScreenHeight
        {
            get
            {
                return GlobalSettings.Default.GraphicsHeight;
            }
        }
    }
}
