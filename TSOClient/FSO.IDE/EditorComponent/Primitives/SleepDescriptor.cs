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
    public class SleepDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Control; } }
        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.Done; } }
        public override Type OperandType { get { return typeof(VMSleepOperand); } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMSleepOperand)Operand;
            var result = new StringBuilder();
            result.Append("for ticks in ");
            result.Append(scope.GetVarScopeDataName(SimAntics.Engine.Scopes.VMVariableScope.Parameters, op.StackVarToDec));
            return result.ToString();
        }

        public override void PopulateOperandView(BHAVEditor master, EditorScope escope, TableLayoutPanel panel)
        {
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, 
                new OpStaticTextProvider("Sleeps for the ticks specified by the chosen parameter, decrementing it towards zero. "
                + "Can be interrupted by the Notify Out Of Idle primitive.")));
            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Ticks in Parameter:", "StackVarToDec", new OpStaticNamedPropertyProvider(escope.GetVarScopeDataNames(VMVariableScope.Parameters))));
        }
    }
}
