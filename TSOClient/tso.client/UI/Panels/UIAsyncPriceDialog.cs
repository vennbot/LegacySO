
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
using FSO.Client.UI.Model;

namespace FSO.Client.UI.Panels
{
    public class UIAsyncPriceDialog : UIDialog
    {
        public delegate void SalePriceDelegate(uint SalePrice);
        public event SalePriceDelegate OnPriceChange;

        public UITextEdit ForSalePrice { get; set; }
        public UITextEdit topText { get; set; }

        public UIAsyncPriceDialog(string itemName, uint originalPrice) : base(UIDialogStyle.Standard| UIDialogStyle.OK | UIDialogStyle.Close, false)
        {
            var script = RenderScript("asyncprice.uis");
            SetSize(240, 286);

            var bg = script.Create<UIImage>("OwnerPriceBack");
            AddAt(3, bg);

            topText.CurrentText = GameFacade.Strings.GetString("239", "2", new string[] { itemName });
            ForSalePrice.CurrentText = originalPrice.ToString();
            OKButton.OnButtonClick += OKClicked;
            CloseButton.OnButtonClick += CloseClicked;

            GameFacade.Screens.inputManager.SetFocus(ForSalePrice);
        }

        private void CloseClicked(UIElement button)
        {
            UIScreen.RemoveDialog(this);
        }

        private void OKClicked(UIElement button)
        {
            uint result = 0;
            if (uint.TryParse(ForSalePrice.CurrentText, out result))
            {
                OnPriceChange?.Invoke(result);
                UIScreen.RemoveDialog(this);
            } else
            {
                HIT.HITVM.Get().PlaySoundEvent(UISounds.Error);
            }
        }
    }
}
