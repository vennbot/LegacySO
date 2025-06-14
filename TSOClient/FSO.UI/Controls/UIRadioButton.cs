
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
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FSO.Client.UI.Controls
{
    public class UIRadioButton : UIButton
    {
        private string _RadioGroup;
        public object RadioData { get; set; }

        public UIRadioButton() : base(GetTexture(0x0000049C00000001)) {
        }

        public UIRadioButton(Texture2D texture) : base(texture){
        }

        public string RadioGroup
        {
            get { return _RadioGroup; }
            set
            {
                if (_RadioGroup != null && value == null){
                    OnButtonClick -= HandleRadioClick;
                }
                _RadioGroup = value;
                if(_RadioGroup != null){
                    OnButtonClick += HandleRadioClick;
                }
            }
        }

        private void HandleRadioClick(UIElement btn){
            var parent = this.Parent;
            if (parent == null) { return; }

            this.Selected = true;

            var group = GetRadioGroup(this.RadioGroup);
            
            foreach (var child in group){
                if (child != this){
                    child.Selected = false;
                }
            }
        }

        public List<UIRadioButton> GetRadioGroup(string group)
        {
            var result = new List<UIRadioButton>();
            _FindRadioGroup(UIScreen.Current, group, result);
            return result;
        }

        private void _FindRadioGroup(UIContainer container, string group, List<UIRadioButton> targetList)
        {
            foreach(var child in container.GetChildren())
            {
                if(child is UIRadioButton && ((UIRadioButton)child).RadioGroup == group)
                {
                    targetList.Add((UIRadioButton)child);
                }else if(child is UIContainer)
                {
                    _FindRadioGroup((UIContainer)child, group, targetList);
                }
            }
        }
    }
}
