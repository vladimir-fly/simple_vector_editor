using System.Collections.Generic;

namespace SVE.Models
{
    public class BaseLayout : ILayout
    {
        public IList<Point> Points { get; private set; }
        public Point Point1 { get; private set; }
        public Point Point2 { get; private set; }

        public BaseLayout(IList<Point> points)
        {
            Points = points;
            Point1 = Points[0];
            Point2 = Points[1];
        }

        public BaseLayout(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
            Points = new List<Point> {Point1, Point2};
        }
    }
}