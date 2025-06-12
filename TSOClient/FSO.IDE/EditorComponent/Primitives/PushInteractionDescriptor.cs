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
using FSO.SimAntics.Engine.Primitives;
using System;
using System.Text;
using System.Windows.Forms;

namespace FSO.IDE.EditorComponent.Primitives
{
    public class PushInteractionDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Control; } }

        public override Type OperandType { get { return typeof(VMPushInteractionOperand); } }

        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.TrueFalse; } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMPushInteractionOperand)Operand;
            var result = new StringBuilder();

            result.Append("Interaction #" + op.Interaction + " of object in ");
            result.Append(op.ObjectInLocal ? "local" : "param");
            result.Append("["+op.ObjectLocation+"]\r\n");

            var flagStr = new StringBuilder();
            string prepend = "";
            if (op.UseCustomIcon) { flagStr.Append("Use Custom Icon in Local "+op.IconLocation); prepend = ", "; }
            if (op.PushTailContinuation) { flagStr.Append(prepend + "Tail Continuation"); prepend = ", "; }
            if (op.PushHeadContinuation) { flagStr.Append(prepend + "Head Continuation"); prepend = ", "; }
            //flagStr.Append(op.Flags);

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
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, new OpStaticTextProvider("Pushes an interaction onto the sim in the stack object. The callee can be an object stored in a parameter (default) or local, indexed by 'Target'.")));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Interaction Num:", "Interaction", new OpStaticValueBoundsProvider(0, 255)));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Target:", "ObjectLocation", new OpStaticValueBoundsProvider(0, 255)));
            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Priority:", "Priority", new OpStaticNamedPropertyProvider(typeof(VMPushPriority))));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Icon Location:", "IconLocation", new OpStaticValueBoundsProvider(0, 255)));
            panel.Controls.Add(new OpValueControl(master, escope, Operand, "Object Location:", "ObjectLocation", new OpStaticValueBoundsProvider(0, 255)));

            panel.Controls.Add(new OpFlagsControl(master, escope, Operand, "Flags:", new OpFlag[] {
                new OpFlag("Use Custom Icon", "UseCustomIcon"),
                new OpFlag("Object In Local", "ObjectInLocal"),
                new OpFlag("Tail Continuation", "PushTailContinuation"),
                new OpFlag("Head Continuation", "PushHeadContinuation")
                }));
            
        }

    }
}
