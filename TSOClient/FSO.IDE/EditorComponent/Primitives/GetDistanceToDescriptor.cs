
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
    public class GetDistanceToDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Math; } }

        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.Done; } }

        public override Type OperandType { get { return typeof(VMGetDistanceToOperand); } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMGetDistanceToOperand)Operand;
            var result = new StringBuilder();

            result.Append("From object with ID: ");
            result.Append(scope.GetVarName(op.ObjectScope, (short)op.OScopeData));
            result.Append("\r\nto the Stack Object\r\nStore in: ");
            result.Append(scope.GetVarName(VMVariableScope.Temps, (short)op.TempNum));

            return result.ToString();
        }

        public override void PopulateOperandView(BHAVEditor master, EditorScope escope, TableLayoutPanel panel)
        {
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, new OpStaticTextProvider("Finds the direction between the specified object to the Stack Object and stores it in the specified Variable Scope. Result is 0-7 inclusive, 0 being North and the following going clockwise in 45 degree increments.")));
            panel.Controls.Add(new OpScopeControl(master, escope, Operand, "From Object:", "ObjectScope", "OScopeData"));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Store in Temp:", "TempNum", new OpStaticValueBoundsProvider(0, 19)));
        }
    }
}
