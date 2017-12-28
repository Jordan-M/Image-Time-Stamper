using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTimeStamp
{
    public partial class Form1 : Form
    {
        readonly string DESKTOP = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        Stamp stamp = new Stamp(new SolidBrush(Color.Red), new Font("Arial", 24, FontStyle.Bold));
        public Form1()
        {
            InitializeComponent();
        }

        private void TimeStampImage(string imagePath, string savePath, Stamp stamp)
        {
            string fileName = Path.Combine(savePath, ExtractFileName(imagePath));
            if (File.Exists(fileName))
            {
                Console.WriteLine("Skipped: " + fileName);
                return;
            }

            try
            {
                using (Bitmap image = new Bitmap(imagePath))
                {
                    using (TimeStamper stamper = new TimeStamper(image, stamp))
                    {
                        try
                        {
                            stamper.TimeStamp().Save(fileName);
                        }
                        catch (ArgumentException ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        Console.WriteLine(imagePath);
                    }
                }
            }
            catch (ArgumentException)
            {
                return;
            }
        }

        private string ExtractFileName(string filename)
        {
            return filename.Substring(filename.LastIndexOf("\\") + 1);
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
            foreach(FileInfo file in files)
            {
                string savePath = Path.Combine(destFolder, file.Name);
                TimeStampImage(file.FullName, destFolder, stamp);
            }

            foreach(DirectoryInfo subdir in dirs)
            {
                TraverseFolder(subdir.FullName, Path.Combine(destFolder, subdir.Name));
            }
        }

        private void IsImage()
        {

        }

        private void uxOpenFolderButton_Click(object sender, EventArgs e)
        {
            if (uxOpenFolderDialog.ShowDialog() == DialogResult.OK)
            {
                if (uxSaveFolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string workingPath = Path.Combine(uxSaveFolderBrowser.SelectedPath, "Time Stamped Images");

                    if (!Directory.Exists(workingPath))
                        Directory.CreateDirectory(workingPath);

                    Task.Factory.StartNew(() => TraverseFolder(uxOpenFolderDialog.SelectedPath, workingPath)).ContinueWith(task => MessageBox.Show("Finished copying files. Report generated at: " + workingPath));
                }
            }
        }

        private void uxOpenFileButton_Click(object sender, EventArgs e)
        {
            if (uxOpenFile.ShowDialog() == DialogResult.OK)
            {
                if (uxSaveFolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    TimeStampImage(uxOpenFile.FileName, uxSaveFolderBrowser.SelectedPath, stamp);
                }
            }
        }
    }
}
