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
using FSO.Client.UI.Framework;

namespace FSO.Client.UI.Controls
{
    public class UIGridViewerRender : UIContainer
    {
        private UIButton button;
        private UIImage image;
        private UIGridViewer owner;
        private object data;

        public UIGridViewerRender(UIGridViewer owner)
        {
            this.owner = owner;

            button = new UIButton(owner.ThumbButtonImage);
            button.Size = owner.ThumbSize;
            button.OnButtonClick += new ButtonClickDelegate(button_OnButtonClick);
            this.Add(button);

            image = new UIImage();
            //image.ScaleX = owner.ThumbImageSize.X / (owner.ThumbSize.X - (owner.ThumbImageOffsets.X * 2));
            image.SetSize(owner.ThumbSize.X - (owner.ThumbImageOffsets.X * 2),
                          owner.ThumbSize.Y - (owner.ThumbImageOffsets.Y * 2));
            image.Position = owner.ThumbImageOffsets;
            this.Add(image);
        }


        void button_OnButtonClick(UIElement button)
        {
            if (data != null)
            {
                owner.SelectedItem = data;
            }
        }

        /// <summary>
        /// Sets the data object for this item render
        /// </summary>
        /// <param name="data"></param>
        public void SetData(object data)
        {
            this.data = data;

            if (data is UIGridViewerItem)
            {
                var castData = ((UIGridViewerItem)data);
                image.Texture = castData.Thumb.Get();
            }
        }


        public void SetSelected(bool selected)
        {
            button.Selected = selected;
        }
    }
}
