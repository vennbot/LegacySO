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
using FSO.IDE.EditorComponent.Model;
using FSO.IDE.EditorComponent.OperandForms;
using FSO.IDE.EditorComponent.OperandForms.DataProviders;
using FSO.SimAntics.Primitives;
using System;
using System.Windows.Forms;

namespace FSO.IDE.EditorComponent.Primitives
{
    public class UnknownPrimitiveDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveRegistry.GetGroupOf((byte)PrimID); } }

        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.TrueFalse; } }

        public override Type OperandType { get { return typeof(VMSubRoutineOperand); } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMSubRoutineOperand)Operand;
            return (op.Arg0 & 0xFF).ToString("x2") + " " + (op.Arg0 >> 8).ToString("x2") + " " +
                (op.Arg1 & 0xFF).ToString("x2") + " " + (op.Arg1 >> 8).ToString("x2") + " " +
                (op.Arg2 & 0xFF).ToString("x2") + " " + (op.Arg2 >> 8).ToString("x2") + " " +
                (op.Arg3 & 0xFF).ToString("x2") + " " + (op.Arg3 >> 8).ToString("x2") + " ";
        }

        public override void PopulateOperandView(BHAVEditor master, EditorScope escope, TableLayoutPanel panel)
        {
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, new OpStaticTextProvider("This primitive is not implemented, but its hexadecimal values may be edited directly.")));
            var provider = new OpStaticValueBoundsProvider(-32768, 32767);
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Argument 1:", "Arg0", provider, true));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Argument 2:", "Arg1", provider, true));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Argument 3:", "Arg2", provider, true));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Argument 4:", "Arg3", provider, true));
        }

    }
}
