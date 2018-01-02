/* Author: Jordan Mryyan
 * MetadataExtractor.cs
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;

namespace ImageTimeStamp
{
    /// <summary>
    /// Extracts EXIF Data from Image files
    /// </summary>
    public static class MetadataExtractor
    {
        /// <summary>
        /// Regex used to split date from EXIF data, we initialize it here so we do have to create a new one for all
        /// the pictures and stress the garbage man.
        /// </summary>
        private static Regex regex = new Regex(":");

        /// <summary>
        /// Extracts the time stamp from an Image file
        /// </summary>
        /// <param name="image">Image to extract time stamp from</param>
        /// <returns></returns>
        public static string ExtractTimeStamp(Image image)
        {
            const int datetimeID = 36867;
            const int digitalTimeID = 36868;

            PropertyItem prop = null;
    
            try
            {
                prop = image.GetPropertyItem(datetimeID);
            }
            catch (ArgumentException)
            {
                try
                {
                    prop = image.GetPropertyItem(digitalTimeID);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Image does not have a date/time field.");
                }
            }

            return regex.Replace(Encoding.UTF8.GetString(prop.Value), "-", 2);
        }

        public static int ExtractOrientation(Image image)
        {
            const int orientationId = 274;
            try
            {
                image.GetPropertyItem(orientationId);
            }
            catch (ArgumentException)
            {
                return 0;
            }

            return (int)image.GetPropertyItem(orientationId).Value[0];
        }
    }
}
