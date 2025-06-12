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
using FSO.SimAntics;
using FSO.Common.Rendering.Framework.Model;
using FSO.Common;
using FSO.SimAntics.NetPlay.Model.Commands;

namespace FSO.Client.UI.Panels
{
    class UITimeOutDialog : UIDialog
    {
        /// <summary>
        /// Exit buttons
        /// </summary>
        public UIButton CloseButton { get; set; }
        public UILabel CounterText { get; set; }
        public VM CallingVM;
        public int Timer;
        public int SubTimer;

        public UITimeOutDialog(VM callingVM, int timer)
            : base(UIDialogStyle.Standard, true)
        {
            this.RenderScript("timeoutdialog.uis");
            this.SetSize(380, 180);

            CloseButton.OnButtonClick += CloseButton_OnButtonClick;
            CallingVM = callingVM;
            Timer = timer;
        }

        public override void Update(UpdateState state)
        {
            base.Update(state);
            SubTimer++;
            if (SubTimer >= FSOEnvironment.RefreshRate)
            {
                Timer--;
                if (Timer <= 0) ForceDC();
                UpdateTimer();
                SubTimer = 0;
            }
        }

        private void UpdateTimer()
        {
            CounterText.Caption = (Timer/(60*60)).ToString().PadLeft(2, '0') + ":"+((Timer/60)%60).ToString().PadLeft(2, '0')+":"+(Timer%60).ToString().PadLeft(2, '0');
        }

        private void CloseButton_OnButtonClick(Framework.UIElement button)
        {
            CallingVM.SendCommand(new VMNetTimeoutNotifyCmd());
            UIScreen.RemoveDialog(this);
        }

        private void ForceDC()
        {
            UIScreen.RemoveDialog(this);
            FSOFacade.Controller.Disconnect(false);
        }
    }
}
