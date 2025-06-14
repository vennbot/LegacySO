
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
using FSO.Client;
using FSO.Vitaboy;
using FSO.SimAntics.Engine.Scopes;

namespace FSO.IDE.Common
{
    public partial class AvatarAnimatorControl : FSOUIControl
    {
        public UIAvatarAnimator Renderer;
        public void EnsureReady()
        {
            if (FSOUI == null)
            {
                var mainCont = new UIExternalContainer(128, 128);
                mainCont.UseZ = true;
                Renderer = new UIAvatarAnimator();
                mainCont.Add(Renderer);
                GameFacade.Screens.AddExternal(mainCont);

                SetUI(mainCont);
            }
        }

        public void ShowAnim(string anim)
        {
            EnsureReady();
            Renderer.SetAnimation(anim);
        }

        public void BindOutfit(VMPersonSuits type, Outfit oft)
        {
            EnsureReady();
            Renderer.BindOutfit(type, oft);
        }

        public void AddAccessory(string name)
        {
            EnsureReady();
            Renderer.AddAccessory(name);
        }

        public void RemoveAccessory(string name)
        {
            EnsureReady();
            Renderer.RemoveAccessory(name);
        }

        public void ClearAccessories()
        {
            EnsureReady();
            Renderer.ClearAccessories();
        }
    }
}
