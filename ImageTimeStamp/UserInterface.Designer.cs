namespace ImageTimeStamp
{
    partial class UserInterface
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
            this.uxOpenFolderButton = new System.Windows.Forms.Button();
            this.uxOpenFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.uxOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.uxOpenFileButton = new System.Windows.Forms.Button();
            this.uxSaveFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.uxProgressBar = new System.Windows.Forms.ProgressBar();
            this.uxProgressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uxOpenFolderButton
            // 
            this.uxOpenFolderButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uxOpenFolderButton.Location = new System.Drawing.Point(12, 72);
            this.uxOpenFolderButton.Name = "uxOpenFolderButton";
            this.uxOpenFolderButton.Size = new System.Drawing.Size(123, 23);
            this.uxOpenFolderButton.TabIndex = 0;
            this.uxOpenFolderButton.Text = "Select Folder";
            this.uxOpenFolderButton.UseVisualStyleBackColor = true;
            this.uxOpenFolderButton.Click += new System.EventHandler(this.uxOpenFolderButton_ClickAsync);
            // 
            // uxOpenFileButton
            // 
            this.uxOpenFileButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uxOpenFileButton.Location = new System.Drawing.Point(149, 72);
            this.uxOpenFileButton.Name = "uxOpenFileButton";
            this.uxOpenFileButton.Size = new System.Drawing.Size(123, 23);
            this.uxOpenFileButton.TabIndex = 1;
            this.uxOpenFileButton.Text = "Select File";
            this.uxOpenFileButton.UseVisualStyleBackColor = true;
            this.uxOpenFileButton.Click += new System.EventHandler(this.uxOpenFileButton_Click);
            // 
            // uxProgressBar
            // 
            this.uxProgressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uxProgressBar.Location = new System.Drawing.Point(12, 12);
            this.uxProgressBar.Name = "uxProgressBar";
            this.uxProgressBar.Size = new System.Drawing.Size(260, 23);
            this.uxProgressBar.TabIndex = 2;
            // 
            // uxProgressLabel
            // 
            this.uxProgressLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uxProgressLabel.AutoSize = true;
            this.uxProgressLabel.Location = new System.Drawing.Point(129, 38);
            this.uxProgressLabel.Name = "uxProgressLabel";
            this.uxProgressLabel.Size = new System.Drawing.Size(24, 13);
            this.uxProgressLabel.TabIndex = 3;
            this.uxProgressLabel.Text = "0/0";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 108);
            this.Controls.Add(this.uxProgressLabel);
            this.Controls.Add(this.uxProgressBar);
            this.Controls.Add(this.uxOpenFileButton);
            this.Controls.Add(this.uxOpenFolderButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 147);
            this.MinimumSize = new System.Drawing.Size(300, 147);
            this.Name = "UserInterface";
            this.Text = "Photo Time Stamper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxOpenFolderButton;
        private System.Windows.Forms.FolderBrowserDialog uxOpenFolderDialog;
        private System.Windows.Forms.OpenFileDialog uxOpenFile;
        private System.Windows.Forms.Button uxOpenFileButton;
        private System.Windows.Forms.FolderBrowserDialog uxSaveFolderBrowser;
        private System.Windows.Forms.ProgressBar uxProgressBar;
        private System.Windows.Forms.Label uxProgressLabel;
    }
}

