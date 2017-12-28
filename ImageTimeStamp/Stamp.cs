using System.Drawing;

namespace ImageTimeStamp
{
    class Stamp
    {
        private Font _font;
        private SolidBrush _brush;

        public Font StampFont
        {
            get { return _font; }
            set { _font = value; }
        }
        public SolidBrush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }

        public Stamp(SolidBrush brush, Font font)
        {
            _font = font;
            _brush = brush;
        }
    }
}
