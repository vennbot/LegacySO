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
using FSO.Client;
using FSO.IDE.Common.Debug;
using FSO.Files.RC;

namespace FSO.IDE.Common
{
    public partial class Debug3DControl : FSOUIControl
    {
        private UI3DDGRP Renderer;
        public DGRP3DMesh Mesh
        {
            get
            {
                return Renderer.TargetComp3D.Mesh;
            }
        }

        public void ShowObject(uint GUID)
        {
            if (FSOUI == null)
            {
                var mainCont = new UIExternalContainer(128, 128);
                mainCont.UseZ = true;
                Renderer = new UI3DDGRP(GUID);
                mainCont.Add(Renderer);
                GameFacade.Screens.AddExternal(mainCont);

                SetUI(mainCont);
            }
            else
            {
                //reuse existing
                lock (FSOUI)
                {
                    Renderer.SetGUID(GUID);
                }
            }
        }

        public void ChangeWorld(int rotation, int zoom)
        {
            lock (FSOUI)
            {
                Renderer.ChangeWorld(rotation, zoom);
            }
        }

        public void ChangeGraphic(int gfx)
        {
            lock (FSOUI)
            {
                Renderer.ChangeGraphic(gfx);
            }
        }

        public void ForceUpdate()
        {
            lock (FSOUI)
            {
                Renderer.ForceUpdate();
            }
        }

        public void SetDynamic(int i)
        {
            lock (FSOUI)
            {
                Renderer.SetDynamic(i);
            }
        }
    }
}
