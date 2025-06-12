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
    public class TransferFundsDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Sim; } }
        public override PrimitiveReturnTypes Returns { get { return PrimitiveReturnTypes.TrueFalse; } }
        public override Type OperandType { get { return typeof(VMTransferFundsOperand); } }

        public override string GetBody(EditorScope scope)
        {
            var op = (VMTransferFundsOperand)Operand;
            return op.TransferType.ToString() + " - " + op.ExpenseType.ToString();
        }

        public override void PopulateOperandView(BHAVEditor master, EditorScope escope, TableLayoutPanel panel)
        {
            panel.Controls.Add(new OpLabelControl(master, escope, Operand,
                new OpStaticTextProvider("Transfers funds from one object to another (either can be 'maxis', essentially an infinite bank). Both must be in the database.")));
            panel.Controls.Add(new OpScopeControl(master, escope, Operand, "Amount Source: ", "AmountOwner", "AmountData"));

            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Transfer Type:", "TransferType", new OpStaticNamedPropertyProvider(typeof(VMTransferFundsType))));
            panel.Controls.Add(new OpComboControl(master, escope, Operand, "Expense Type:", "ExpenseType", new OpStaticNamedPropertyProvider(typeof(VMTransferFundsExpenseType))));

            panel.Controls.Add(new OpFlagsControl(master, escope, Operand, "Flags:", new OpFlag[] {
                new OpFlag("Just Test", "JustTest"),
                new OpFlag("Subtract", "Subtract"),
            }));
        }
    }
}
