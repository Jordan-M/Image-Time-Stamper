using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageTimeStamp
{
    class TimeStamper : IDisposable
    {
        private Bitmap _image;
        private Graphics _graphics;
        private Stamp _stamp;

        public Bitmap Image { get; }
        public Graphics Graphic { get; }


        public TimeStamper(Bitmap image, Stamp stamp)
        {
            if (image == null)
                throw new ArgumentNullException();

            _graphics = Graphics.FromImage(image);
            _graphics.SmoothingMode = SmoothingMode.AntiAlias;
            _image = image;
            _stamp = stamp;
        }

        public void Dispose()
        {
            _image.Dispose();
            _graphics.Dispose();
        }

        public Bitmap TimeStamp()
        {
            string dateTime;
            try
            {
                dateTime = MetadataExtractor.ExtractTimeStamp(_image);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

            int orientation = MetadataExtractor.ExtractOrientation(_image);

             _stamp.StampFont = FontHelper.GetAdjustedFont(_graphics, dateTime, _stamp.StampFont, 1500, 100, 24, true);

            if (orientation == 3)
                    return StampOrientation3(dateTime, _stamp);
            else
                    return StampOrientation1(dateTime, _stamp);
        }

        private Bitmap StampOrientation1(string timestamp, Stamp stamp)
        {
            _graphics.DrawString(timestamp, stamp.StampFont, stamp.Brush, new Point(0, 0));
            return _image;
        }


        private Bitmap StampOrientation3(string timestamp, Stamp stamp)
        {
            /* new Point(0, 0) // BOTTOM RIGHT
               new Point(0, bitMapImage.Height - 75) // TOP RIGHT
               new Point(bitMapImage.Width - 125, bitMapImage.Height - 75) //TOP LEFT
               new Point(bitMapImage.Width - 125, 0) // BOTTOM LEFT '
            */

            GraphicsState state = _graphics.Save();
            _graphics.ResetTransform();
            _graphics.RotateTransform(180);
            _graphics.TranslateTransform(_image.Width - 75, _image.Height - 45, MatrixOrder.Append);
            _graphics.DrawString(timestamp, stamp.StampFont, stamp.Brush, new Point(0, 0));
            _graphics.Restore(state);
            return _image;
        }
    }
}
