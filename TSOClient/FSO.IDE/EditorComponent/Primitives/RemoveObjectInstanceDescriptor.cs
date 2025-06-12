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
using FSO.IDE.EditorComponent.Model;
using FSO.IDE.EditorComponent.OperandForms;
using FSO.IDE.EditorComponent.OperandForms.DataProviders;
using FSO.SimAntics.Engine.Primitives;
using System;
using System.Text;
using System.Windows.Forms;

namespace FSO.IDE.EditorComponent.Primitives
{
    public class RemoveObjectInstanceDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Object ; } }

        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.TrueFalse; } }

        public override Type OperandType { get { return typeof(VMRemoveObjectInstanceOperand); } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMRemoveObjectInstanceOperand)Operand;
            var result = new StringBuilder();
            
            result.Append(EditorScope.Behaviour.Get<STR>(156).GetString((int)op.Target));

            var flagStr = new StringBuilder();
            string prepend = "";
            if (op.ReturnImmediately) { flagStr.Append(prepend + "Return Immediately"); prepend = ", "; }
            if (op.CleanupAll) { flagStr.Append(prepend + "Cleanup Multitile"); prepend = ", "; }

            if (flagStr.Length != 0)
            {
                result.Append("\r\n(");
                result.Append(flagStr);
                result.Append(")");
            }
            return result.ToString();
        }

        public override void PopulateOperandView(BHAVEditor master, EditorScope escope, TableLayoutPanel panel)
        {
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, new OpStaticTextProvider("Removes the specified object instance.")));
            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Target Object:", "Target", new OpStaticNamedPropertyProvider(EditorScope.Behaviour.Get<STR>(156))));

            panel.Controls.Add(new OpFlagsControl(master, escope, Operand, "Flags:", new OpFlag[] {
                new OpFlag("Return Immediately", "ReturnImmediately"),
                new OpFlag("Cleanup Multitile", "CleanupAll"),
                }));
        }
    }
}
