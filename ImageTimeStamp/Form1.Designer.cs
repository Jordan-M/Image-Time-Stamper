namespace ImageTimeStamp
{
    partial class Form1
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
            this.SuspendLayout();
            // 
            // uxOpenFolderButton
            // 
            this.uxOpenFolderButton.Location = new System.Drawing.Point(3, 118);
            this.uxOpenFolderButton.Name = "uxOpenFolderButton";
            this.uxOpenFolderButton.Size = new System.Drawing.Size(123, 23);
            this.uxOpenFolderButton.TabIndex = 0;
            this.uxOpenFolderButton.Text = "Select Folder";
            this.uxOpenFolderButton.UseVisualStyleBackColor = true;
            this.uxOpenFolderButton.Click += new System.EventHandler(this.uxOpenFolderButton_Click);
            // 
            // uxOpenFileButton
            // 
            this.uxOpenFileButton.Location = new System.Drawing.Point(149, 118);
            this.uxOpenFileButton.Name = "uxOpenFileButton";
            this.uxOpenFileButton.Size = new System.Drawing.Size(123, 23);
            this.uxOpenFileButton.TabIndex = 1;
            this.uxOpenFileButton.Text = "Select File";
            this.uxOpenFileButton.UseVisualStyleBackColor = true;
            this.uxOpenFileButton.Click += new System.EventHandler(this.uxOpenFileButton_Click);
            // 
            // uxProgressBar
            // 
            this.uxProgressBar.Location = new System.Drawing.Point(3, 89);
            this.uxProgressBar.Name = "uxProgressBar";
            this.uxProgressBar.Size = new System.Drawing.Size(269, 23);
            this.uxProgressBar.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 201);
            this.Controls.Add(this.uxProgressBar);
            this.Controls.Add(this.uxOpenFileButton);
            this.Controls.Add(this.uxOpenFolderButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxOpenFolderButton;
        private System.Windows.Forms.FolderBrowserDialog uxOpenFolderDialog;
        private System.Windows.Forms.OpenFileDialog uxOpenFile;
        private System.Windows.Forms.Button uxOpenFileButton;
        private System.Windows.Forms.FolderBrowserDialog uxSaveFolderBrowser;
        private System.Windows.Forms.ProgressBar uxProgressBar;
    }
}

