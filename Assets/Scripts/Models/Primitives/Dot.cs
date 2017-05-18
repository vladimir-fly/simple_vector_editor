using System.Collections.Generic;

namespace SVE.Models
{
    public class Dot : ILayout
    {
        public IList<Point> Points { get; private set; }
        public Dot(Point point)
        {
            Points = new[] { point };
        }
    }
}