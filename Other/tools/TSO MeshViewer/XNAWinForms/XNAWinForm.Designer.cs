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
namespace Dressup
{
    partial class XNAWinForm
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
            this.panelViewport = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelViewport
            // 
            this.panelViewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelViewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewport.Location = new System.Drawing.Point(0, 0);
            this.panelViewport.Name = "panelViewport";
            this.panelViewport.Size = new System.Drawing.Size(518, 377);
            this.panelViewport.TabIndex = 1;
            this.panelViewport.Resize += new System.EventHandler(this.OnViewportResize);
            this.panelViewport.BackColorChanged += new System.EventHandler(this.panelViewport_BackColorChanged);
            this.panelViewport.Paint += new System.Windows.Forms.PaintEventHandler(this.OnVieweportPaint);
            // 
            // XNAWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 377);
            this.Controls.Add(this.panelViewport);
            this.DoubleBuffered = true;
            this.Name = "XNAWinForm";
            this.Text = "XNA WinForms";
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelViewport;

    }
}

