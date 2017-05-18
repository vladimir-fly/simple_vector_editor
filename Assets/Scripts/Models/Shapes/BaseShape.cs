namespace SVE.Models
{
    public class BaseShape : Shape
    {
        public Point Point1 { get; private set; }
        public Point Point2 { get; private set; }

        public BaseShape(BaseLayout layout, EShapeType shapeType, Color color, Color fillColor) :
            base(new ILayout[] {layout}, shapeType, color, fillColor)
        {
            if (layout == null) return;
            Point1 = layout.Point1;
            Point2 = layout.Point2;
        }
    }
}