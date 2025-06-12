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
using FSO.Client.UI.Framework.Parser;

namespace FSO.Client.UI.Panels.EODs.Archetypes
{
    public abstract class UIBasicEOD : UIEOD
    {
        private string UIScriptPath;

        protected string EODName;
        protected UIScript Script;

        public UIBasicEOD(UIEODController controller, string name, string uiScript) : base(controller)
        {
            EODName = name;
            UIScriptPath = uiScript;

            InitUI();
            InitEOD();
        }

        protected virtual void InitUI()
        {
            Script = RenderScript(UIScriptPath);
        }

        protected virtual void InitEOD()
        {
            PlaintextHandlers[EODName + "_show"] = Show;
        }

        protected abstract EODLiveModeOpt GetEODOptions();

        protected virtual void Show(string evt, string txt)
        {
            EODController.ShowEODMode(GetEODOptions());
        }

        public override void OnClose()
        {
            Send("close", "");
            base.OnClose();
        }
    }
}
