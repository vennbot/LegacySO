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

namespace FSO.Client.UI.Panels.EODs
{
    public class UICooldownEventEOD : UIEOD
    {
        public UICooldownEventEOD(UIEODController controller) : base(controller)
        {
            PlaintextHandlers["success"] = SuccessTest;
            PlaintextHandlers["failure"] = FailureTest;
        }
        
        public void SuccessTest(string evt, string data)
        {
            UIAlert alert = null;
            alert = UIScreen.GlobalShowAlert(new UIAlertOptions()
            {
                TextSize = 12,
                Title = "Success Test",
                Message = "You successfully passed the cooldown test." + System.Environment.NewLine +
                "You cannot use this item until: " + data,
                Alignment = TextAlignment.Center,
                TextEntry = false,
                Buttons = UIAlertButton.Ok((btn) =>
                {
                    UIScreen.RemoveDialog(alert);
                }),

            }, true);
            CloseInteraction();
        }
        public void FailureTest(string evt, string data)
        {
            UIAlert alert = null;
            alert = UIScreen.GlobalShowAlert(new UIAlertOptions()
            {
                TextSize = 12,
                Title = "Fail Test",
                Message = "You failed the cooldown test." + System.Environment.NewLine +
                "You cannot use this item until: " + data,
                Alignment = TextAlignment.Center,
                TextEntry = false,
                Buttons = UIAlertButton.Ok((btn) =>
                {
                    UIScreen.RemoveDialog(alert);
                }),

            }, true);
            CloseInteraction();
        }
    }
}
