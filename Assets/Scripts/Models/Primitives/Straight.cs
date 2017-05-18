using System.Collections.Generic;

namespace SVE.Models
{
    public class Straight : ILayout, IColorable
    {
        public IList<Point> Points { get; private set; }
        public Color Color { get; private set; }

        public Straight(IList<Point> points, Color color)
        {
            Points = points;
            Color = color;
        }

        public void SetColor(Color color)
        {
            Color = color;
        }
    }
}