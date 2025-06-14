
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
using FSO.IDE.EditorComponent.OperandForms.DataProviders;

namespace FSO.IDE.EditorComponent.OperandForms
{
    public partial class OpValueControl : UserControl, IOpControl
    {
        private OpValueBoundsProvider BoundsProvider;
        private BHAVEditor Master;
        private VMPrimitiveOperand Operand;
        private EditorScope Scope;
        private string Property;

        private bool IgnoreSet;

        public OpValueControl()
        {
            InitializeComponent();
        }

        public OpValueControl(BHAVEditor master, EditorScope scope, VMPrimitiveOperand operand, string title, string property, OpValueBoundsProvider bounds, bool isHex = false)
        {
            InitializeComponent();
            Master = master;
            Scope = scope;
            Operand = operand;
            TitleLabel.Text = title;
            ValueEntry.Hexadecimal = isHex;
            Property = property;
            BoundsProvider = bounds;
            this.Dock = DockStyle.Fill;
            OperandUpdated();
        }

        public void OperandUpdated()
        {
            var bounds = BoundsProvider.GetBounds(Scope, Operand);
            ValueEntry.Minimum = bounds[0];
            ValueEntry.Maximum = bounds[1];

            var prop = OpUtils.GetOperandProperty(Operand, Property);
            if (prop.GetType() == typeof(uint)) prop = unchecked((int)Convert.ToUInt32(prop));
            int value = Convert.ToInt32(prop);

            IgnoreSet = true;
            ValueEntry.Value = value;
            IgnoreSet = false;
        }

        private void ValueEntry_ValueChanged(object sender, EventArgs e)
        {
            if (IgnoreSet) return;
            OpUtils.SetOperandProperty(Operand, Property, ValueEntry.Value);

            Master.SignalOperandUpdate();
        }

    }
}
