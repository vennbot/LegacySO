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
using FSO.Client;
using FSO.Client.UI.Framework;
using FSO.Files.Formats.IFF.Chunks;
using FSO.IDE.EditorComponent.Commands;
using FSO.IDE.EditorComponent.UI;
using FSO.SimAntics;
using FSO.SimAntics.Engine;

namespace FSO.IDE.EditorComponent
{
    public class BHAVViewControl : FSOUIControl
    {
        public UIBHAVEditor Editor;
        public BHAVContainer Cont
        {
            get
            {
                return (Editor==null)?null:Editor.BHAVView;
            }
        }

        public BHAVViewControl() : base()
        {
            
        }

        public void InitBHAV(BHAV bhav, EditorScope scope, VMEntity debugEnt, VMStackFrame debugFrame, BHAVPrimSelect callback)
        {
            if (FSOUI == null)
            {
                var mainCont = new UIExternalContainer(1024, 768);
                Editor = new UIBHAVEditor(bhav, scope, debugEnt);
                mainCont.Add(Editor);
                GameFacade.Screens.AddExternal(mainCont);

                SetUI(mainCont);
                Editor.BHAVView.OnSelectedChanged += callback;
            } else
            {
                //reuse existing
                lock (FSOUI)
                {
                    Editor.QueueCommand(new ChangeBHAVCommand(bhav, scope, debugFrame, callback));
                }
            }
        }
    }
}
