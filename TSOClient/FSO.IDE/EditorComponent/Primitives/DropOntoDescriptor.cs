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
using FSO.SimAntics.Engine.Scopes;
using FSO.SimAntics.Primitives;
using System;
using System.Text;
using System.Windows.Forms;

namespace FSO.IDE.EditorComponent.Primitives
{
    public class DropOntoDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Position; } }

        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.TrueFalse; } }

        public override Type OperandType { get { return typeof(VMDropOntoOperand); } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMDropOntoOperand)Operand;
            var result = new StringBuilder();

            result.Append("From slot[");
            if (op.SrcSlotMode == 1) result.Append(scope.GetVarName(VMVariableScope.Parameters, (short)op.SrcSlotNum));
            else result.Append(op.SrcSlotNum);
            result.Append("] to slot[");
            if (op.DestSlotMode == 1) result.Append(scope.GetVarName(VMVariableScope.Parameters, (short)op.DestSlotNum));
            else result.Append(op.DestSlotNum);
            result.Append("]");

            return result.ToString();
        }

        public override void PopulateOperandView(BHAVEditor master, EditorScope escope, TableLayoutPanel panel)
        {
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, new OpStaticTextProvider("Drops an object from the specified slot on the Caller to a destination slot on the Stack Object.")));
            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Source Mode: ", "SrcSlotMode", new OpStaticNamedPropertyProvider(new string[] { "Slot[literal]", "Slot[parameter]" }, 0)));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Source ID: ", "SrcSlotNum", new OpStaticValueBoundsProvider(0, 65535)));

            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Destination Mode: ", "DestSlotMode", new OpStaticNamedPropertyProvider(new string[] { "Slot[literal]", "Slot[parameter]" }, 0)));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Destination ID: ", "DestSlotNum", new OpStaticValueBoundsProvider(0, 65535)));
        }
    }
}
