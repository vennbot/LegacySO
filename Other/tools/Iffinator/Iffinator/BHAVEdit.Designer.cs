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
namespace Iffinator
{
    partial class BHAVEdit
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LstInstructions = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // LstInstructions
            // 
            this.LstInstructions.FormattingEnabled = true;
            this.LstInstructions.Location = new System.Drawing.Point(12, 12);
            this.LstInstructions.Name = "LstInstructions";
            this.LstInstructions.Size = new System.Drawing.Size(205, 381);
            this.LstInstructions.TabIndex = 0;
            // 
            // BHAVEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 402);
            this.Controls.Add(this.LstInstructions);
            this.Name = "BHAVEdit";
            this.Text = "BHAV Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LstInstructions;
    }
}
