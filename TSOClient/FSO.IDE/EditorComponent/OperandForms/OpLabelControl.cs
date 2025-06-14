
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
using FSO.IDE.EditorComponent.OperandForms.DataProviders;
using FSO.SimAntics.Engine;
using System.Windows.Forms;

namespace FSO.IDE.EditorComponent.OperandForms
{
    public class OpLabelControl : Label, IOpControl
    {
        private OpTextProvider TextProvider;
        private VMPrimitiveOperand Operand;
        private EditorScope Scope;

        public OpLabelControl()
        {
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.AutoSize = true;
        }

        public OpLabelControl(BHAVEditor master, EditorScope scope, VMPrimitiveOperand operand, OpTextProvider textP) : this()
        {
            Scope = scope;
            Operand = operand;
            TextProvider = textP;
            OperandUpdated();
        }

        public void OperandUpdated()
        {
            this.Text = TextProvider.GetText(Scope, Operand);
        }
    }
}
