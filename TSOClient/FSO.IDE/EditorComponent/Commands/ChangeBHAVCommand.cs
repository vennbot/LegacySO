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
using FSO.Files.Formats.IFF.Chunks;
using FSO.IDE.EditorComponent.UI;
using FSO.SimAntics.Engine;

namespace FSO.IDE.EditorComponent.Commands
{
    public class ChangeBHAVCommand : BHAVCommand
    {
        BHAV Target;
        EditorScope TargetScope;
        VMStackFrame Frame;

        BHAV Old;
        EditorScope OldScope;
        VMStackFrame OldFrame;

        BHAVPrimSelect SelectCallback;
        bool KeepScroll;

        public ChangeBHAVCommand(BHAV target, EditorScope scope, VMStackFrame frame, BHAVPrimSelect callback)
        {
            Target = target;
            TargetScope = scope;
            Frame = frame;
            SelectCallback = callback;
        }

        public override void Execute(BHAV bhav, UIBHAVEditor editor)
        {
            Old = editor.BHAVView.EditTarget;
            OldScope = editor.BHAVView.Scope;
            OldFrame = editor.DebugFrame;
            editor.BHAVView.OnSelectedChanged -= SelectCallback;
            editor.SwitchBHAV(Target, TargetScope, Frame);
            editor.BHAVView.OnSelectedChanged += SelectCallback;
        }

        public override void Undo(BHAV bhav, UIBHAVEditor editor)
        {
            editor.BHAVView.OnSelectedChanged -= SelectCallback;
            editor.SwitchBHAV(Old, OldScope, OldFrame);
            editor.BHAVView.OnSelectedChanged += SelectCallback;
        }
    }
}
