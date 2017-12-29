using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTimeStamp
{
    public partial class Form1 : Form
    {
        private static Stamp stamp = new Stamp(new SolidBrush(Color.Red), new Font("Arial", 24, FontStyle.Bold));
        private static TimeStamper timeStamper = new TimeStamper();

        public Form1()
        {
            InitializeComponent();
        }

        private void uxOpenFileButton_Click(object sender, EventArgs e)
        {
            if (uxOpenFile.ShowDialog() == DialogResult.OK)
            {
                if (uxSaveFolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    TimeStamp(uxOpenFile.FileName, uxSaveFolderBrowser.SelectedPath, stamp);
                }
            }
        }

        private async void uxOpenFolderButton_ClickAsync(object sender, EventArgs e)
        {
            if (uxOpenFolderDialog.ShowDialog() == DialogResult.OK)
            {
                if (uxSaveFolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    ResetUI();

                    await TraverseFolderAsync(uxOpenFolderDialog.SelectedPath, uxSaveFolderBrowser.SelectedPath);
                    MessageBox.Show("Finished copying files. Report generated at root of Time Stamped Images folder.");

                    ResetUI();
                }
            }
        }

        private async Task TraverseFolderAsync(string readPath, string writePath)
        {
            string workingPath = Path.Combine(writePath, "Time Stamped Images");

            if (!Directory.Exists(workingPath))
                Directory.CreateDirectory(workingPath);

            await Task.Factory.StartNew(() => TraverseFolder(readPath, workingPath));
        }

        private void TraverseFolder(string folder, string destFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(folder);

            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string savePath = Path.Combine(destFolder, file.Name);
                TimeStamp(file.FullName, destFolder, stamp);

                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        uxProgressBar.Value++;
                        uxProgressLabel.Text = uxProgressBar.Value + "/" + uxProgressBar.Maximum;
                    }));
                }
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                TraverseFolder(subdir.FullName, Path.Combine(destFolder, subdir.Name));
            }

        }

        private void TimeStamp(string imagePath, string savePath, Stamp stamp)
        {
            timeStamper.Stamp(imagePath, savePath, stamp);
        }

        private void ResetUI()
        {
            ToggleUIButtons();
            ResetProgress();
        }

        private void ResetProgress()
        {
            uxProgressBar.Value = 0;
            uxProgressBar.Maximum = FileHelper.CalculateNumFiles(uxOpenFolderDialog.SelectedPath, true);
            uxProgressLabel.Text = "0/" + uxProgressBar.Maximum;
        }

        private void ToggleUIButtons()
        {
            uxOpenFolderButton.Enabled = !uxOpenFolderButton.Enabled;
            uxOpenFileButton.Enabled = !uxOpenFileButton.Enabled;
        }
    }
}
