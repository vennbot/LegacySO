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
using FSO.SimAntics;
using FSO.SimAntics.Model.Platform;
using Microsoft.Xna.Framework;
using System;

namespace FSO.Client.UI.Panels.Neighborhoods
{
    public class UIDonatorDialog : UIDialog
    {
        // ------------- Community Lot Buy Mode -------------
        //         [ ] Place Objects     [ ] Donate
        // (0/100) Mayor Objects, (1999/2000) Donated Objects

        private UILotControl LotParent;
        private UILabel SummaryLabel;

        private int LastMayorObj = -1;
        private int LastDonatedObj = -1;

        private string[] ModeNames = new string[]
        {
            "Place Objects",
            "Donate"
        };

        public UIRadioButton[] ModeButtons = new UIRadioButton[2];

        public UIDonatorDialog(UILotControl panel) : base(UIDialogStyle.Standard, false)
        {
            LotParent = panel;
            var modeHBox = new UIHBoxContainer();
            modeHBox.Spacing = 20;

            for (int i = 0; i < 2; i++) {
                var hbox = new UIHBoxContainer();
                var radio = new UIRadioButton();
                radio.RadioGroup = "donateMode";
                radio.RadioData = false;
                radio.Selected = (i == 0);
                radio.Tooltip = ModeNames[i];
                var donate = (i > 0);
                radio.OnButtonClick += (btn) =>
                {
                    SetDonate(donate);
                };
                ModeButtons[i] = radio;

                hbox.Add(radio);
                hbox.Add(new UILabel
                {
                    Caption = ModeNames[i]
                });
                modeHBox.Add(hbox);
            }

            modeHBox.AutoSize();
            modeHBox.Position = new Vector2((400 - modeHBox.Size.X) / 2, 35);
            Add(modeHBox);

            SetSize(400, 100);

            Add(SummaryLabel = new UILabel()
            {
                Caption = "(100/100) Mayor Objects, (1999/2000) Donated Objects",
                Position = new Vector2(200, 70),
                Size = Vector2.One,
                Alignment = TextAlignment.Center | TextAlignment.Middle
            });

            GameResized();

            Caption = "Community Lot Buy Mode";
        }

        public override void Update(UpdateState state)
        {
            base.Update(state);

            //only update if we're visible
            //...

            var objLimit = LotParent.vm.TSOState.ObjectLimit;
            var donatedLimit = LotParent.vm.TSOState.DonateLimit;

            var objCount = LotParent.vm.Context.ObjectQueries.NumUserObjects;
            var donatedCount = LotParent.vm.Context.ObjectQueries.NumDonatedObjects;

            if (objCount != LastMayorObj || donatedCount != LastDonatedObj)
            {
                SummaryLabel.Caption = $"({objCount}/{objLimit}) Mayor Objects, ({donatedCount}/{donatedLimit}) Donated Objects";

                LastMayorObj = objCount;
                LastDonatedObj = donatedCount;
            }

            var mode = LotParent.vm.PlatformState.Validator.GetPurchaseMode(PurchaseMode.Normal, (VMAvatar)LotParent.ActiveEntity, 0, true);
            if (Visible && mode == PurchaseMode.Disallowed) Visible = false;
            var force = mode == PurchaseMode.Donate;
            if (force)
            {
                LotParent.ObjectHolder.DonateMode = force;
                ModeButtons[0].Disabled = true;
            } else
            {
                ModeButtons[0].Disabled = false;
            }

            var active = Array.FindIndex(ModeButtons, x => x.Selected);
            if ((active == 1) != LotParent.ObjectHolder.DonateMode)
            {
                for (int i=0; i<2; i++)
                {
                    ModeButtons[i].Selected = ((i == 1) == LotParent.ObjectHolder.DonateMode);
                }
            }
        }

        public void SetDonate(bool mode)
        {
            LotParent.ObjectHolder.DonateMode = mode;
        }

        public override void GameResized()
        {
            base.GameResized();
            var screen = UIScreen.Current;
            Position = new Vector2((screen.ScreenWidth - 400) / 2, 40);
        }
    }
}
