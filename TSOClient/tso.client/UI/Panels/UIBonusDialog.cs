
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
using FSO.Common.DatabaseService.Model;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FSO.Client.UI.Panels
{
    public class UIBonusDialog : UIDialog
    {
        public UIListBox BonusListBox { get; set; }
        public UIListBox BonusTotalsListBox { get; set; }
        public UISlider BonusListSlider { get; set; }
        public UIButton BonusListScrollUpButton { get; set; }
        public UIButton BonusListScrollDownButton { get; set; }
        public UILabel TextMessage { get; set; }

        public UIButton ColumnButton1 { get; set; }
        public UIButton ColumnButton2 { get; set; }
        public UIButton ColumnButton3 { get; set; }
        public UIButton ColumnButton4 { get; set; }
        public UIButton ColumnButton5 { get; set; }

        public UIBonusDialog(List<LoadAvatarBonus> bonus)
            : base(UIDialogStyle.Close, true)
        {
            this.Opacity = 0.9f;

            var script = this.RenderScript("visitorbonusdialog.uis");
            this.DialogSize = (Point)script.GetControlProperty("DialogSize");
            this.Caption = (string)script["TitleString"];

            BonusListBox.AttachSlider(BonusListSlider);
            BonusListSlider.AttachButtons(BonusListScrollUpButton, BonusListScrollDownButton, 1);

            var bg1 = new UIImage(UITextBox.StandardBackground);
            var bg2 = new UIImage(UITextBox.StandardBackground);

            script.ApplyControlProperties(bg1, "BonusListBoxBackground");
            script.ApplyControlProperties(bg2, "BonusTotalsBackground");

            Add(bg1);
            Add(bg2);

            //bring to front
            Add(TextMessage);
            Add(BonusListBox); 
            Add(BonusListSlider);
            Add(BonusListScrollUpButton);
            Add(BonusListScrollDownButton);
            Add(BonusTotalsListBox);
            Add(ColumnButton1);
            Add(ColumnButton2);
            Add(ColumnButton3);
            Add(ColumnButton4);
            Add(ColumnButton5);

            var listStyle = script.Create<UIListBoxTextStyle>("BonusListBoxColors", BonusListBox.FontStyle);
            BonusListBox.TextStyle = listStyle;
            BonusTotalsListBox.TextStyle = listStyle;


            BonusListBox.Items = bonus.Select(x =>
            {
                return new UIListBoxItem(x, x.Date, "$" + x.VisitorBonus , "$" + x.PropertyBonus, "$" + x.SimBonus, "$" + (x.VisitorBonus + x.PropertyBonus + x.SimBonus));
            }).ToList();

            BonusTotalsListBox.Items = new List<UIListBoxItem>()
            {
                new UIListBoxItem(null, script.GetString("TotalsText"), "$" + bonus.Sum(x => x.VisitorBonus), "$" + bonus.Sum(x => x.PropertyBonus), "$" + bonus.Sum(x => x.SimBonus), "$" + bonus.Sum(x => x.PropertyBonus + x.SimBonus + x.VisitorBonus))
            };

            CloseButton.OnButtonClick += CloseButton_OnButtonClick;
        }

        private void CloseButton_OnButtonClick(Framework.UIElement button)
        {
            UIScreen.RemoveDialog(this);
        }
    }
}
