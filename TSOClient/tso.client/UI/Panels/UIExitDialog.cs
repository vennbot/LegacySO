
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

namespace FSO.Client.UI.Panels
{
    public class UIExitDialog : UIDialog
    {
        /// <summary>
        /// Exit buttons
        /// </summary>
        public UIButton ReLoginButton { get; set; }
        public UIButton ExitButton { get; set; }
        public UIButton CancelButton { get; set; }

        public UIExitDialog()
            : base(UIDialogStyle.Standard, true)
        {
            this.RenderScript("exitdialog.uis");
            this.SetSize(380, 180);

            ExitButton.OnButtonClick += new ButtonClickDelegate(ExitButton_OnButtonClick);
            CancelButton.OnButtonClick += new ButtonClickDelegate(CancelButton_OnButtonClick);
            ReLoginButton.OnButtonClick += new ButtonClickDelegate(ReLoginButton_OnButtonClick);
        }

        private void ExitButton_OnButtonClick(UIElement button)
        {
            GameFacade.Kill();
        }

        private void CancelButton_OnButtonClick(UIElement button)
        {
            UIScreen.RemoveDialog(this);
        }

        private void ReLoginButton_OnButtonClick(UIElement button)
        {
            FSOFacade.Controller.Disconnect(true);
        }
    }
}
