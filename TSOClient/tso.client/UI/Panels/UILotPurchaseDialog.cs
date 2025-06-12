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
using FSO.Client.Controllers;
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;
using FSO.Common.Utils;
using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;

namespace FSO.Client.UI.Panels
{
    public class UILotPurchaseDialog : UIDialog
    {
        private Regex VALIDATE_NUMERIC = new Regex("[0-9]");
        private Regex VALIDATE_SPECIAL_CHARS = new Regex(@"[^\p{L} '-]");
        private Regex VALIDATE_APOSTROPHES = new Regex("^[^']*'?[^']*$");
        private Regex VALIDATE_DASHES = new Regex("^[^-]*-?[^-]*$");
        private Regex VALIDATE_SPACES = new Regex("^[^ ]+(?: [^ ]+)*$");

        public UITextEdit NameTextEdit { get; set; }
        public UIValidationMessages<string> NameTextEditValidation { get; set; }
        public UILabel MessageText { get; set; }

        public string TextTitle { get; set; }
        public string InvalidNameErrorTitle { get; set; }
        public string InvalidNameErrorShort { get; set; }
        public string InvalidNameErrorLong { get; set; }
        public string InvalidNameErrorNumeric { get; set; }
        public string InvalidNameErrorApostrophe { get; set; }
        public string InvalidNameErrorDash { get; set; }
        public string InvalidNameErrorSpace { get; set; }
        public string InvalidNameErrorSpecial { get; set; }
        public string InvalidNameErrorCensor { get; set; }
        public string CloseButtonTooltip { get; set; }
        public string AcceptButtonTooltip { get; set; }

        public event Callback<string> OnNameChosen;

        public UILotPurchaseDialog() : base(UIDialogStyle.Standard| UIDialogStyle.OK | UIDialogStyle.Close, false)
        {
            var script = RenderScript("lotpurchasedialog.uis");
            SetSize(380, 210);
            
            NameTextEdit = script.Create<UITextEdit>("NameTextEdit");
            NameTextEdit.MaxLines = 1;
            NameTextEdit.BackgroundTextureReference = UITextBox.StandardBackground;
            NameTextEdit.TextMargin = new Rectangle(8, 3, 8, 3);
            NameTextEdit.FlashOnEmpty = true;
            NameTextEdit.MaxChars = 24;
            Add(NameTextEdit);

            NameTextEditValidation = new UIValidationMessages<string>()
                .WithValidation(InvalidNameErrorShort, x => x.Length < 3)
                .WithValidation(InvalidNameErrorLong, x => x.Length > 24)
                .WithValidation(InvalidNameErrorNumeric, x => VALIDATE_NUMERIC.IsMatch(x))
                .WithValidation(InvalidNameErrorApostrophe, x => !VALIDATE_APOSTROPHES.IsMatch(x))
                .WithValidation(InvalidNameErrorDash, x => !VALIDATE_DASHES.IsMatch(x))
                .WithValidation(InvalidNameErrorSpace, x => !VALIDATE_SPACES.IsMatch(x))
                .WithValidation(InvalidNameErrorSpecial, x => VALIDATE_SPECIAL_CHARS.IsMatch(x));

            NameTextEditValidation.ErrorPrefix = InvalidNameErrorTitle;
            NameTextEditValidation.Position = new Vector2(NameTextEdit.X, NameTextEdit.Y + NameTextEdit.Height);
            NameTextEditValidation.Width = (int)NameTextEdit.Width;
            DynamicOverlay.Add(NameTextEditValidation);

            GameFacade.Screens.inputManager.SetFocus(NameTextEdit);

            NameTextEdit.OnChange += NameTextEdit_OnChange;
            RefreshValidation();

            OKButton.OnButtonClick += AcceptButton_OnButtonClick;
            CloseButton.OnButtonClick += CloseButton_OnButtonClick;
        }

        private void CloseButton_OnButtonClick(Framework.UIElement button)
        {
            //todo: special behaviour?
            UIScreen.RemoveDialog(this);
        }

        private void AcceptButton_OnButtonClick(Framework.UIElement button)
        {
            if (OnNameChosen != null)
            {
                OnNameChosen(NameTextEdit.CurrentText);
            }
            else
            {
                FindController<TerrainController>().PurchaseLot(NameTextEdit.CurrentText);
            }
        }

        private void NameTextEdit_OnChange(Framework.UIElement element)
        {
            RefreshValidation();
        }

        private void RefreshValidation()
        {
            var valid = NameTextEditValidation.Validate(NameTextEdit.CurrentText);
            OKButton.Disabled = !valid;
        }
    }
}
