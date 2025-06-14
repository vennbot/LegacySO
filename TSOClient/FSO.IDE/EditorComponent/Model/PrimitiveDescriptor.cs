
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
using FSO.IDE.EditorComponent.OperandForms;
using FSO.SimAntics.Engine;
using System;
using System.Windows.Forms;

namespace FSO.IDE.EditorComponent.Model
{
    public abstract class PrimitiveDescriptor
    {
        public ushort PrimID;
        public VMPrimitiveOperand Operand;

        public abstract PrimitiveGroup Group { get; }
        public abstract PrimitiveReturnTypes Returns { get; }
        public abstract Type OperandType { get; }

        public virtual string GetTitle(EditorScope scope) {
            var primName = EditorScope.Behaviour.Get<STR>(139).GetString(PrimID);
            return (primName == null)?"Primitive #"+PrimID:primName;
        }
        public abstract string GetBody(EditorScope scope);

        public virtual void PopulateOperandView(BHAVEditor master, EditorScope escope, TableLayoutPanel panel)
        {
            panel.Controls.Add(new OpUnknownControl());
        }

        //TODO: modifiable operand models, special form controls for specific types.
    }

    public enum PrimitiveReturnTypes
    {
        TrueFalse,
        Done,
    }
}
