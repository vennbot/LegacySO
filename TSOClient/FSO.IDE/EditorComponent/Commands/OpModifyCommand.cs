
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

namespace FSO.IDE.EditorComponent.Commands
{
    public class OpModifyCommand : BHAVCommand
    {
        public PrimitiveBox Prim;
        public byte[] NewOp;
        public byte[] OldOp;

        public OpModifyCommand(PrimitiveBox prim, byte[] newOp)
        {
            Prim = prim;
            NewOp = newOp;
            OldOp = prim.Instruction.Operand;
        }

        public override void Execute(BHAV bhav, UIBHAVEditor editor)
        {
            Prim.Instruction.Operand = NewOp;
            Prim.UpdateDisplay();
            Content.Content.Get().Changes.ChunkChanged(bhav);
            FSO.SimAntics.VM.BHAVChanged(bhav);
        }

        public override void Undo(BHAV bhav, UIBHAVEditor editor)
        {
            Prim.Instruction.Operand = OldOp;
            Prim.RefreshOperand();
            Prim.UpdateDisplay();
            Content.Content.Get().Changes.ChunkChanged(bhav);
            FSO.SimAntics.VM.BHAVChanged(bhav);
        }
    }
}
