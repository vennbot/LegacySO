
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
using FSO.Server.Protocol.Electron.Model;

namespace FSO.Client.UI.Panels
{
    public class UIModMenu : UIDialog
    {
        private UIImage Background;
        private UIButton IPBanButton;

        public uint AvatarID;

        public UIModMenu() : base(UIDialogStyle.Tall | UIDialogStyle.Close, true)
        {
            SetSize(380, 300);
            Caption = "Do what to this user?";

            Position = new Microsoft.Xna.Framework.Vector2(
                (GlobalSettings.Default.GraphicsWidth / 2.0f) - (480/2),
                (GlobalSettings.Default.GraphicsHeight / 2.0f) - 150
            );

            IPBanButton = new UIButton();
            IPBanButton.Caption = "IP Ban";
            IPBanButton.Position = new Microsoft.Xna.Framework.Vector2(40, 50);
            IPBanButton.Width = 300;
            IPBanButton.OnButtonClick += x =>
            {
                var controller = FindController<FSO.Client.Controllers.CoreGameScreenController>();
                if (controller != null)
                    controller.ModRequest(AvatarID, ModerationRequestType.IPBAN_USER);
                UIScreen.RemoveDialog(this);
            };
            Add(IPBanButton);

            var BanButton = new UIButton();
            BanButton.Caption = "Ban User";
            BanButton.Position = new Microsoft.Xna.Framework.Vector2(40, 90);
            BanButton.Width = 300;
            BanButton.OnButtonClick += x =>
            {
                var controller = FindController<FSO.Client.Controllers.CoreGameScreenController>();
                if (controller != null)
                    controller.ModRequest(AvatarID, ModerationRequestType.BAN_USER);
                UIScreen.RemoveDialog(this);
            };
            Add(BanButton);

            var kickButton = new UIButton();
            kickButton.Caption = "Kick Avatar";
            kickButton.Position = new Microsoft.Xna.Framework.Vector2(40, 130);
            kickButton.Width = 300;
            kickButton.OnButtonClick += x =>
            {
                var controller = FindController<FSO.Client.Controllers.CoreGameScreenController>();
                if (controller != null)
                    controller.ModRequest(AvatarID, ModerationRequestType.KICK_USER);
                UIScreen.RemoveDialog(this);
            };
            Add(kickButton);

            var nhoodBanButton = new UIButton();
            nhoodBanButton.Caption = "Nhood Ban";
            nhoodBanButton.Position = new Microsoft.Xna.Framework.Vector2(40, 170);
            nhoodBanButton.Width = 300;
            nhoodBanButton.OnButtonClick += x =>
            {
                var controller = FindController<FSO.Client.Controllers.CoreGameScreenController>();

                UIAlert.Prompt("Neighborhood Gameplay Ban", "Ban this user for how long? (in days, 0 for perma)", true, (result) =>
                {
                    if (result == null) return;
                    uint dayCount;
                    if (!uint.TryParse(result, out dayCount))
                    {
                        UIAlert.Alert("Neighborhood Gameplay Ban", "Invalid number of days. Please try again.", true);
                    }
                    uint untilDate = (dayCount == 0) ? uint.MaxValue : ClientEpoch.Now + dayCount * 60 * 60 * 24;

                    UIAlert.Prompt("Neighborhood Gameplay Ban", "What message do you want to leave? (optional)", true, (result2) =>
                    {
                        if (result2 == null) return;
                        if (controller != null)
                        {
                            controller.NeighborhoodProtocol.BanUser(AvatarID, untilDate, result2, (code) =>
                            {
                                //response
                                if (code == Server.Protocol.Electron.Packets.NhoodResponseCode.SUCCESS)
                                {
                                    UIAlert.Alert("Neighborhood Gameplay Ban", "Ban has been submitted. Note that if you ban someone twice your second ban will overwrite the first.", true);
                                }
                            });
                        }
                    });
                });
            };
            Add(nhoodBanButton);

            CloseButton.OnButtonClick += CloseButton_OnButtonClick;
        }

        private void CloseButton_OnButtonClick(UIElement button)
        {
            UIScreen.RemoveDialog(this);
        }
    }
}
