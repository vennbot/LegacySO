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
    public class FindBestObjectForFunctionDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Control; } }

        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.TrueFalse; } }

        public override Type OperandType { get { return typeof(VMFindBestObjectForFunctionOperand); } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMFindBestObjectForFunctionOperand)Operand;
            var result = new StringBuilder();

            result.Append("Find Best Object For Function: ");
            var function = EditorScope.Behaviour.Get<STR>(201).GetString((int)op.Function);
            result.Append(function);

            return result.ToString();
        }

        public override void PopulateOperandView(BHAVEditor master, EditorScope escope, TableLayoutPanel panel)
        {
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, new OpStaticTextProvider("Finds the 'best' object for a specified entry point, ranked by a combination of distance and the relevant score in its Object Data.")));
            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Function: ", "Function", new OpStaticNamedPropertyProvider(EditorScope.Behaviour.Get<STR>(201), 0)));
        }
    }
}
