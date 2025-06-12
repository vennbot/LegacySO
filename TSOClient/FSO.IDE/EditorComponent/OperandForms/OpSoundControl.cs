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
using FSO.SimAntics.Primitives;
using FSO.Files.Formats.IFF.Chunks;

namespace FSO.IDE.EditorComponent.OperandForms
{
    public partial class OpSoundControl : UserControl, IOpControl
    {
        private BHAVEditor Master;
        private VMPlaySoundOperand Operand;
        private EditorScope Scope;

        private string IndexProperty;
        private string SourceProperty;

        public OpSoundControl()
        {
            InitializeComponent();
        }

        public OpSoundControl(BHAVEditor master, EditorScope scope, VMPrimitiveOperand operand, string title)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            Master = master;
            Scope = scope;
            Operand = (VMPlaySoundOperand)operand;
            TitleLabel.Text = title;

            OperandUpdated();
        }

        public void OperandUpdated()
        {
            var op = Operand;
            var fwav = Scope.GetResource<FWAV>(op.EventID, ScopeSource.Private);
            ObjectLabel.Text = (fwav == null) ? "*Event Missing!*" : fwav.Name;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var popup = new VarSoundSelect(Scope.Object.Resource.MainIff, Operand.EventID);
            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                Operand.EventID = (ushort)popup.SelectedFWAV;
            }
            Master.SignalOperandUpdate();
        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
