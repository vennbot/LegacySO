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
namespace FSOInstaller
{
    partial class SetupFetcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupFetcher));
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 26);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(260, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Location = new System.Drawing.Point(12, 10);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(260, 13);
            this.StatusLabel.TabIndex = 2;
            this.StatusLabel.Text = "Downloading Manifest...";
            // 
            // SetupFetcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 61);
            this.ControlBox = false;
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.ProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupFetcher";
            this.Text = "Initializing...";
            this.Load += new System.EventHandler(this.SetupFetcher_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label StatusLabel;
    }
}

