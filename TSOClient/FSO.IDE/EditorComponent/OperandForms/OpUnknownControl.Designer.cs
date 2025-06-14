
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
namespace FSO.IDE.EditorComponent.OperandForms
{
    partial class OpUnknownControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UnkLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UnkLabel
            // 
            this.UnkLabel.AutoSize = true;
            this.UnkLabel.Location = new System.Drawing.Point(0, 0);
            this.UnkLabel.Name = "UnkLabel";
            this.UnkLabel.Size = new System.Drawing.Size(97, 13);
            this.UnkLabel.TabIndex = 0;
            this.UnkLabel.Text = "Unknown Operand";
            this.UnkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UnkLabel.Click += new System.EventHandler(this.UnkLabel_Click);
            // 
            // OpUnknownControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.UnkLabel);
            this.Name = "OpUnknownControl";
            this.Size = new System.Drawing.Size(100, 13);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UnkLabel;
    }
}
