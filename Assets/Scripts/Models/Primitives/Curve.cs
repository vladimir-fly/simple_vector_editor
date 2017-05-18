using System.Collections.Generic;

namespace SVE.Models
{
    public class Curve : ILayout, IColorable
    {
        public IList<Point> Points { get; private set; }
        public Color Color { get; private set; }

        public Curve(IList<Point> points, Color color)
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