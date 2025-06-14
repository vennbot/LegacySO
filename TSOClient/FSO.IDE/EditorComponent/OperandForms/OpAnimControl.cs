
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
using System;
using System.Windows.Forms;
using FSO.SimAntics.Engine;
using FSO.SimAntics.Engine.Primitives;
using FSO.IDE.EditorComponent.Primitives;

namespace FSO.IDE.EditorComponent.OperandForms
{
    public partial class OpAnimControl : UserControl, IOpControl
    {
        private BHAVEditor Master;
        private VMAnimateSimOperand Operand;
        private EditorScope Scope;

        private string IndexProperty;
        private string SourceProperty;

        public OpAnimControl()
        {
            InitializeComponent();
        }

        public OpAnimControl(BHAVEditor master, EditorScope scope, VMPrimitiveOperand operand, string title)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            Master = master;
            Scope = scope;
            Operand = (VMAnimateSimOperand)operand;
            TitleLabel.Text = title;

            OperandUpdated();
        }

        public void OperandUpdated()
        {
            var op = Operand;
            if (op.AnimationID == 0)
                ObjectLabel.Text = "Stop Animation";
            else
                ObjectLabel.Text = AnimateSimDescriptor.GetAnimationName(Scope, op.Source, op.AnimationID);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var popup = new VarAnimSelect(AnimateSimDescriptor.GetAnimTable(Scope, Operand.Source), Operand.AnimationID);
            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                Operand.AnimationID = (ushort)popup.SelectedAnim;
            }
            Master.SignalOperandUpdate();
        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
