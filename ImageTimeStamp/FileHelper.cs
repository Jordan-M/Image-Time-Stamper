using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageTimeStamp
{
    static class FileHelper
    {
        public static string ExtractFileName(string filename)
        {
            return filename.Substring(filename.LastIndexOf("\\") + 1);
        }

        public static int CalculateNumFiles(string location, bool searchSubfolders)
        {
            SearchOption option = (searchSubfolders) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            return Directory.GetFiles(location, "*.*", option).Length;
        }
    }
}
