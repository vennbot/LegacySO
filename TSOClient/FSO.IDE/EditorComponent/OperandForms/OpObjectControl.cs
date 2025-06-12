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
using System;
using System.Windows.Forms;
using FSO.SimAntics.Engine;

namespace FSO.IDE.EditorComponent.OperandForms
{
    public partial class OpObjectControl : UserControl, IOpControl
    {
        private BHAVEditor Master;
        private VMPrimitiveOperand Operand;
        private EditorScope Scope;

        private string GUIDProperty;

        public OpObjectControl()
        {
            
            InitializeComponent();
        }

        public OpObjectControl(BHAVEditor master, EditorScope scope, VMPrimitiveOperand operand, string title, string guidProperty)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            Master = master;
            Scope = scope;
            Operand = operand;
            TitleLabel.Text = title;
            GUIDProperty = guidProperty;

            OperandUpdated();
        }

        public void OperandUpdated()
        {
            uint guid = Convert.ToUInt32(OpUtils.GetOperandProperty(Operand, GUIDProperty));
            var obj = Content.Content.Get().WorldObjects.Get(guid);
            ObjectLabel.Text = (obj == null) ? ("0x"+guid.ToString("X8")) : obj.OBJ.ChunkLabel;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var popup = new VarObjectSelect();
            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                OpUtils.SetOperandProperty(Operand, GUIDProperty, popup.GUIDResult);
            }
            Master.SignalOperandUpdate();
        }
    }
}
