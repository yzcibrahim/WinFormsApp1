using System.Collections.Generic;
using System.Drawing;

namespace WinFormsApp1
{
    public class DestinationBox
    {
        public Rectangle Rect { get; set; }
        public Color C { get { return Colors[ColorIndex]; } }
        public int ColorIndex { get; set; } = 0;
        public List<Color> Colors { get; set; } = new List<Color>();
    }
}
