using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ImageTimeStamp
{
    class TimeStamper
    {
        private Graphics _graphics;
        public Graphics Graphic { get; }

        public void Stamp(string imagePath, string savePath, Stamp stamp)
        {
            string fileName = Path.Combine(savePath, FileHelper.ExtractFileName(imagePath));
            if (File.Exists(fileName))
            {
                // Build report?
                Console.WriteLine("Skipped: " + fileName);
                return;
            }

            try
            {
                using (Bitmap image = new Bitmap(imagePath))
                {
                    try
                    {
                        SetGraphics(image);
                        TimeStamp(image, stamp).Save(fileName);
                    }
                    catch (ArgumentException ex)
                    {
                        // Build report
                    }
                    Console.WriteLine(imagePath);
                }

            }
            catch (ArgumentException) // Wasn't a image file
            {
                return;
            }
        }

        private Bitmap TimeStamp(Bitmap image, Stamp stamp)
        {
            string dateTime;
            try
            {
                dateTime = MetadataExtractor.ExtractTimeStamp(image);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

            int orientation = MetadataExtractor.ExtractOrientation(image);

             stamp.StampFont = FontHelper.GetAdjustedFont(_graphics, dateTime, stamp.StampFont, 1500, 100, 24, true);

            if (orientation == 3)
            {
                return StampOrientation3(image, dateTime, stamp);
            }
            else
            {
                StampOrientation1(dateTime, stamp);
                return image;
            }
        }

        private void StampOrientation1(string timestamp, Stamp stamp)
        {
            _graphics.DrawString(timestamp, stamp.StampFont, stamp.Brush, new Point(0, 0));
        }


        private Bitmap StampOrientation3(Bitmap image, string timestamp, Stamp stamp)
        {
            /* new Point(0, 0) // BOTTOM RIGHT
               new Point(0, bitMapImage.Height - 75) // TOP RIGHT
               new Point(bitMapImage.Width - 125, bitMapImage.Height - 75) //TOP LEFT
               new Point(bitMapImage.Width - 125, 0) // BOTTOM LEFT '
            */

            GraphicsState state = _graphics.Save();
            _graphics.ResetTransform();
            _graphics.RotateTransform(180);
            _graphics.TranslateTransform(image.Width - 75, image.Height - 45, MatrixOrder.Append);
            _graphics.DrawString(timestamp, stamp.StampFont, stamp.Brush, new Point(0, 0));
            _graphics.Restore(state);
            return image;
        }

        private void SetGraphics(Bitmap image)
        {
            if (_graphics != null)
                _graphics.Dispose();

            _graphics = Graphics.FromImage(image);
            _graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
    }
}
