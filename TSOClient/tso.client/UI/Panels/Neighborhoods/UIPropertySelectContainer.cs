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
using FSO.Client.Controllers.Panels;
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;
using FSO.Client.Utils;
using Microsoft.Xna.Framework;

namespace FSO.Client.UI.Panels.Neighborhoods
{
    public class UIPropertySelectContainer : UIContainer
    {
        private UILabel SelectedLotName;
        private UIInboxDropdown Dropdown;
        public uint SelectedLot;

        public UIPropertySelectContainer()
        {
            Add(SelectedLotName = new UILabel()
            {
                Size = new Vector2(341, 25),
                Alignment = TextAlignment.Center | TextAlignment.Top,
                Caption = "<no property selected>"
            });

            Add(Dropdown = new UIInboxDropdown() { Position = new Vector2(0, 25) });
            Dropdown.OnSearch += (query) =>
            {
                FindController<GenericSearchController>()?.SearchLots(query, false, (results) =>
                {
                    Dropdown.SetResults(results);
                });
            };
            Dropdown.OnSelect += SelectLot; ;
            Add(Dropdown);

            var ctr = ControllerUtils.BindController<GenericSearchController>(this);
        }

        private void SelectLot(uint id, string name)
        {
            SelectedLot = id;
            SelectedLotName.Caption = name;
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(0, 0, 341, 80);
        }
    }
}
