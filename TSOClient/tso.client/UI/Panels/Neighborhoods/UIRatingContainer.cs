
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
using FSO.Server.Protocol.Electron.Packets;
using Microsoft.Xna.Framework;

namespace FSO.Client.UI.Panels.Neighborhoods
{
    public class UIRatingContainer : UIContainer
    {
        public UITextEdit Edit;
        public UIRatingDisplay Rating;
        public UILabel CharacterLimitDisplay;
        public UILabel CurrentRatingLabel;

        public UIRatingContainer(bool rating)
        {
            Edit = new UITextEdit();
            Edit.SetSize(420, 75);
            Edit.Mode = UITextEditMode.Editor;
            Edit.OnChange += Edit_OnChange;
            Edit.MaxLines = 4;
            Edit.MaxChars = 140;
            Edit.BackgroundTextureReference = UITextBox.StandardBackground;
            Edit.TextMargin = new Rectangle(8, 2, 8, 3);

            Add(Edit);

            Rating = new UIRatingDisplay(true);
            Rating.Position = new Vector2(5, 80);
            Rating.Settable = true;
            Rating.DisplayStars = 0;
            Rating.Visible = rating;
            Add(Rating);

            CurrentRatingLabel = new UILabel();
            CurrentRatingLabel.Position = new Vector2(75, 77);
            CurrentRatingLabel.CaptionStyle = TextStyle.DefaultTitle.Clone();
            CurrentRatingLabel.CaptionStyle.Shadow = true;
            CurrentRatingLabel.Caption = "0 Stars";
            CurrentRatingLabel.Visible = rating;
            Add(CurrentRatingLabel);

            CharacterLimitDisplay = new UILabel();
            CharacterLimitDisplay.Position = new Vector2(414, 77);
            CharacterLimitDisplay.Alignment = TextAlignment.Right | TextAlignment.Top;
            CharacterLimitDisplay.Size = new Vector2(1, 1);
            CharacterLimitDisplay.CaptionStyle = TextStyle.DefaultTitle.Clone();
            CharacterLimitDisplay.CaptionStyle.Shadow = true;
            CharacterLimitDisplay.Caption = "0/140";
            Add(CharacterLimitDisplay);

            var emoji = new UIEmojiSuggestions(Edit);
            Add(emoji);
        }

        public override void Update(UpdateState state)
        {
            CurrentRatingLabel.Caption = Rating.Tooltip;
            base.Update(state);
        }

        public NhoodRequest GetRequest(NhoodRequest req)
        {
            return new NhoodRequest()
            {
                Type = NhoodRequestType.RATE,
                Message = Edit.CurrentText,
                Value = (uint)Rating.HalfStars,
                TargetAvatar = req.TargetAvatar,
                TargetNHood = req.TargetNHood
            };
        }

        public NhoodRequest GetRunRequest(NhoodRequest req)
        {
            return new NhoodRequest()
            {
                Type = NhoodRequestType.NOMINATION_RUN,
                Message = Edit.CurrentText,
                TargetNHood = req.TargetNHood
            };
        }

        private void Edit_OnChange(UIElement element)
        {
            CharacterLimitDisplay.Caption = Edit.CurrentText.Length+"/140";
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(0, 0, 420, 106);
        }
    }
}
