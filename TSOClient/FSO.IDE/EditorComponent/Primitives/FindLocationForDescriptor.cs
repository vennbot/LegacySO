
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
using FSO.SimAntics.Engine.Scopes;
using FSO.SimAntics.Primitives;
using System;
using System.Text;
using System.Windows.Forms;

namespace FSO.IDE.EditorComponent.Primitives
{
    public class FindLocationForDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Position; } }

        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.TrueFalse; } }

        public override Type OperandType { get { return typeof(VMFindLocationForOperand); } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMFindLocationForOperand)Operand;
            var result = new StringBuilder();
            
            result.Append(EditorScope.Behaviour.Get<STR>(239).GetString((int)op.Mode));

            var flagStr = new StringBuilder();
            string prepend = "";
            if (op.UseLocalAsRef) {
                flagStr.Append("Obj in ");
                flagStr.Append(scope.GetVarName(VMVariableScope.Local, op.Local));
                flagStr.Append(" as reference");
                prepend = ",\r\n";
            }
            if (op.PreferNonEmpty) { flagStr.Append(prepend + "Prefer non-empty"); prepend = ",\r\n"; }
            if (op.UserEditableTilesOnly) { flagStr.Append(prepend + "User Editable tiles only"); prepend = ",\r\n"; }

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
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, new OpStaticTextProvider("Finds a location to place the Stack Object, using a variety of modes and options.")));
            var modes = new OpStaticNamedPropertyProvider(EditorScope.Behaviour.Get<STR>(239));
            modes.EnsureProperty(5, "random");
            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Mode:", "Mode", modes));

            panel.Controls.Add(new OpFlagsControl(master, escope, Operand, "Flags:", new OpFlag[] {
                new OpFlag("Use Local As Ref", "UseLocalAsRef"),
                new OpFlag("Prefer Non-Empty", "PreferNonEmpty"),
                new OpFlag("User Editable Tiles Only", "UserEditableTilesOnly"),
                }));
            panel.Controls.Add(new OpLabelControl(master, escope, Operand, new OpStaticTextProvider("If 'Use Local As Ref' is set, the object with ID specified in the local is used as the starting position instead of the caller.")));
            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Local:", "Local", new OpStaticNamedPropertyProvider(escope.GetVarScopeDataNames(VMVariableScope.Local))));
        }
    }
}
