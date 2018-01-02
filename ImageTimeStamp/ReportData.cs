using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageTimeStamp
{
    public class ReportData
    {
        private  Dictionary<string, int> _fileTypes = new Dictionary<string, int>();
        private List<string> _errors = new List<string>();
        private int _totalFiles = 0;
        private int _totalFilesStamped = 0;
        private string _folderPath = "";

        public int TotalFiles
        {
            get { return _totalFiles; }
            set { _totalFiles = value; }
        }

        public int TotalFilesStamped
        {
            get { return _totalFilesStamped; }
            set { _totalFilesStamped = value; }
        }

        public string FolderPath
        {
            get { return _folderPath; }
            set { _folderPath = value; }
        }

        public Dictionary<string, int> FileTypes
        {
            get { return _fileTypes; }
        }

        public List<string> Errors
        {
            get { return _errors; }
        }
    
        public void AddFileType(string fileType)
        {
            fileType = fileType.ToLower();
            if (_fileTypes.ContainsKey(fileType))
            {
                _fileTypes[fileType]++;
            }
            else
            {
                _fileTypes.Add(fileType, 1);
            }
        }

        public void AddError(string errorPath)
        {
            _errors.Add(errorPath);
        }

        public void Reset()
        {
            _folderPath = "";
            _fileTypes.Clear();
            _errors.Clear();
            _totalFiles = 0;
            _totalFilesStamped = 0;
        }
    }
}
