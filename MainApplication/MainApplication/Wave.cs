using System.Drawing;

namespace MainApplication
{
    public class Wave
    {
        //properties
        public Point Location;
        public Size Size;
        public int MaxHeight; //or the max size of the wave
        public Color Color;
        public int LocationOffset; //to center the wave
        public int Inflate; //the waves will inflate everytime

        //constructor
        public Wave(Point location, Size size, int maxHeight, Color color, int locationOffset, int inflate)
        {
            Location = location;
            Size = size;
            MaxHeight = maxHeight;
            Color = color;
            LocationOffset = locationOffset;
            Inflate = inflate;
        }
    }
}
