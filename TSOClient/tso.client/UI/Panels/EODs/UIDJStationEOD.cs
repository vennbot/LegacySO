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
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FSO.Client.UI.Panels.EODs
{
    public class UIDJStationEOD : UIEOD
    {
        public UIImage DrumsLabel { get; set; }
        public UIImage DrumsPanel { get; set; }
        public UIImage DrumsText { get; set; }

        public UIImage BassLabel { get; set; }
        public UIImage BassPanel { get; set; }
        public UIImage BassText { get; set; }

        public UIImage SynthLabel { get; set; }
        public UIImage SynthPanel { get; set; }
        public UIImage SynthText { get; set; }

        public UIImage VoxLabel { get; set; }
        public UIImage VoxPanel { get; set; }
        public UIImage VoxText { get; set; }

        public Texture2D DJRedImage { get; set; }
        public Texture2D DJBlueImage { get; set; }
        public Texture2D DJGreenImage { get; set; }
        public Texture2D DJYellowImage { get; set; }

        public UILabel DrumsSample { get; set; }
        public UILabel BassSample { get; set; }
        public UILabel SynthSample { get; set; }
        public UILabel VoxSample { get; set; }

        public UIImage TeamMedallion { get; set; }
        public int TeamNumber;
        public List<UIElement> PatternButtons;

        public UIDJStationEOD(UIEODController controller) : base(controller)
        {
            //these will be populated when the script is rendered.
            DrumsLabel = new UIImage();
            DrumsPanel = new UIImage();
            DrumsText = new UIImage();

            BassLabel = new UIImage();
            BassPanel = new UIImage();
            BassText = new UIImage();

            SynthLabel = new UIImage();
            SynthPanel = new UIImage();
            SynthText = new UIImage();

            VoxLabel = new UIImage();
            VoxPanel = new UIImage();
            VoxText = new UIImage();

            Add(DrumsLabel);
            Add(DrumsPanel);
            Add(DrumsText);

            Add(BassLabel);
            Add(BassPanel);
            Add(BassText);

            Add(SynthLabel);
            Add(SynthPanel);
            Add(SynthText);

            Add(VoxLabel);
            Add(VoxPanel);
            Add(VoxText);

            TeamMedallion = new UIImage();
            Add(TeamMedallion);

            var script = this.RenderScript("djstationeod.uis");
            
            var children = ChildrenWithinIdRange(100, 423);
            PatternButtons = children;
            foreach (var child in children)
            {
                var c = child.NumericId.ToString();
                if (child is UIButton)
                {
                    ((UIButton)child).OnButtonClick += (btn) => Pattern(int.Parse(((char)(c[0]-1)).ToString()), int.Parse(c[1].ToString()), int.Parse(c[2].ToString()));
                }
            }

            PlaintextHandlers["dj_show"] = P_Show;
            PlaintextHandlers["dj_active"] = P_Active;
        }

        public void Pattern(int category, int ind1, int ind2)
        {
            Send("press_button", category.ToString()+ind1.ToString()+ind2.ToString());
        }

        public void P_Show(string evt, string txt)
        {
            int.TryParse(txt, out TeamNumber);
            if (TeamNumber < 0 || TeamNumber > 3) TeamNumber = 0;
            TeamMedallion.Texture = (new Texture2D[] { DJRedImage, DJBlueImage, DJGreenImage, DJYellowImage })[TeamNumber];

            EODController.ShowEODMode(new EODLiveModeOpt
            {
                Buttons = 0,
                Expandable = false,
                Height = EODHeight.Tall,
                Length = EODLength.Full,
                Timer = EODTimer.None,
                Tips = EODTextTips.None
            });
        }

        public void P_Active(string evt, string txt)
        {
            var split = txt.Split('|');
            if (split.Length < 4) return;
            foreach (var elem in PatternButtons)
            {
                var btn = (elem as UIButton);
                if (btn != null)
                {
                    var id = elem.NumericId.ToString();
                    int category = (id[0] - '1');
                    int ind1 = (id[1] - '0'); //(ABC), digit 1, digit 2
                    int ind2 = (id[2] - '0');
                    btn.Selected = split[category][ind1] == ind2.ToString()[0];
                }
            }
            var dist = 'A' - '0';

            DrumsSample.Caption = new string(new char[] { (char)(dist + split[0][0]), ' ', (char)(1 + split[0][1]), ' ', (char)(1 + split[0][2]) } );
            BassSample.Caption = new string(new char[] { (char)(dist + split[1][0]), ' ', (char)(1 + split[1][1]), ' ', (char)(1 + split[1][2]) });
            SynthSample.Caption = new string(new char[] { (char)(dist + split[2][0]), ' ', (char)(1 + split[2][1]), ' ', (char)(1 + split[2][2]) });
            VoxSample.Caption = new string(new char[] { (char)(dist + split[3][0]), ' ', (char)(1 + split[3][1]), ' ', (char)(1 + split[3][2]) });
        }

        public override void OnClose()
        {
            CloseInteraction();
            base.OnClose();
        }
    }
}
