/* FileHelper.cs
 * Author: Jordan Mryyan
 */
using System.IO;


namespace ImageTimeStamp
{
    /// <summary>
    /// A collection of methods dealing with files and filepaths
    /// </summary>
    static class FileHelper
    {
        /// <summary>
        /// Gets the name of a file from a path string
        /// </summary>
        /// <param name="filepath">The full path of a file</param>
        /// <returns>The filename</returns>
        public static string ExtractFileName(string filepath)
        {
            return GetAllAfterLast(filepath, '\\');
        }

        /// <summary>
        /// Calculates the total number of files in a directory
        /// </summary>
        /// <param name="location">Directory of files to count</param>
        /// <param name="searchSubfolders">If true, searches all of the subdolders in location else searches only root folder</param>
        /// <returns>Number of files in a given directory</returns>
        public static int CalculateNumFiles(string location, bool searchSubfolders)
        {
            SearchOption option = (searchSubfolders) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            return Directory.GetFiles(location, "*.*", option).Length;
        }

        public static string ExtractFileExt(string filepath)
        {
            return GetAllAfterLast(filepath, '.');
        }

        private static string GetAllAfterLast(string filepath, char c)
        {
            return filepath.Substring(filepath.LastIndexOf(c) + 1);
        }
    }
}
