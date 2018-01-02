using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ImageTimeStamp
{
    class TimeStamper
    {
        private Graphics _graphics;
        public Graphics Graphic { get; }

        public void Stamp(string imagePath, string savePath, Stamp stamp, ReportData report = null)
        {
            string fileName = Path.Combine(savePath, FileHelper.ExtractFileName(imagePath));

            if (!File.Exists(fileName))
            {
                try
                {
                    using (Bitmap image = new Bitmap(imagePath))
                    {
                        try
                        {
                            SetGraphics(image);
                            TimeStamp(image, stamp).Save(fileName);

                            if (report != null)
                                report.TotalFilesStamped += 1;
                        }
                        catch (ArgumentException)
                        {
                            if (report != null)
                                report.AddError(imagePath);
                        }
                    }

                }
                catch (ArgumentException)
                {
                    // Wasn't a image file so just skip it
                }
            }

            if (report != null)
            {
                report.AddFileType(FileHelper.ExtractFileExt(imagePath));
                report.TotalFiles += 1;
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
