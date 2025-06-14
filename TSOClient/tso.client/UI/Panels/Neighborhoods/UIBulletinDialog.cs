
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
using FSO.Client.Controllers.Panels;
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;
using FSO.Client.Utils;
using FSO.Common.DataService.Model;
using FSO.Common.Rendering.Framework.Model;
using FSO.Common.Utils;
using FSO.Files.Formats.tsodata;
using System;
using System.Collections.Generic;

namespace FSO.Client.UI.Panels.Neighborhoods
{
    public class UIBulletinDialog : UIDialog
    {
        protected static UIBulletinDialog GlobalInstance;
        public static bool Present
        {
            get
            {
                return GlobalInstance != null;
            }
        }

        public UIBulletinBoard Board;
        public UIBulletinPost Post;
        public bool ShowingSpecific;
        public event Action<int> OnModeChange;
        public Binding<Neighborhood> CurrentNeigh;

        public float THeight {
            get
            {
                return Background.Height;
            }
            set
            {
                Background.SetSize(Background.Width, (int)value);
            }
        }

        private uint _MayorID;
        public uint MayorID
        {
            get
            {
                return _MayorID;
            }
            set
            {
                _MayorID = value;
                Post.IsMayor = FindController<CoreGameScreenController>()?.IsMe(value) ?? false;
            }
        }

        public UIBulletinDialog(uint nhoodID) : base(UIDialogStyle.Close, true)
        {
            Board = new UIBulletinBoard();
            Board.OnSelection += SelectedPost;
            Post = new UIBulletinPost();
            Post.Opacity = 0;
            Post.Visible = false;
            Post.OnBack += Return;
            DynamicOverlay.Add(Board);
            DynamicOverlay.Add(Post);

            CurrentNeigh = new Binding<Neighborhood>()
                .WithBinding(this, "Caption", "Neighborhood_Name", (name) =>
                {
                    return GameFacade.Strings.GetString("f120", "1", new string[] { (string)name });
                })
                .WithBinding(this, "MayorID", "Neighborhood_MayorID");

            Caption = GameFacade.Strings.GetString("f120", "1", new string[] { "" });
            SetSize(600, 610);

            var controller = ControllerUtils.BindController<BulletinDialogController>(this);
            controller.LoadNhood(nhoodID);

            GlobalInstance = this;
        }

        public override void Removed()
        {
            base.Removed();
            GlobalInstance = null;
        }

        public void SelectedPost(BulletinItem item)
        {
            ShowingSpecific = true;
            Board.AcceptSelections = false;
            Board.Fade(0f);
            Post.AcceptSelections = true;
            Post.SetPost(item);
            Post.Fade(1f);

            OnModeChange?.Invoke((item == null) ? 2 : 1);

            GameFacade.Screens.Tween.To(this, 0.66f, new Dictionary<string, float>() { { "THeight", 550 } }, TweenQuad.EaseOut);
        }

        public void Return()
        {
            ShowingSpecific = false;
            Board.AcceptSelections = true;
            Post.AcceptSelections = false;
            Board.Fade(1f);
            Post.Fade(0f);
            OnModeChange?.Invoke(0);

            GameFacade.Screens.Tween.To(this, 0.66f, new Dictionary<string, float>() { { "THeight", 610 } }, TweenQuad.EaseOut);
        }

        public override void Update(UpdateState state)
        {
            base.Update(state);
            Board.Visible = Board.Opacity > 0;
            Post.Visible = Post.Opacity > 0;
        }
    }
}
