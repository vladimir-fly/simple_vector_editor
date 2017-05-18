using System.Collections.Generic;
using System.Linq;

namespace SVE.Models
{
    public class Shape : IShape
    {
        public IList<ILayout> Layouts { get; protected set; }
        public EShapeType ShapeType { get; protected set; }
        public Color Color { get; protected set; }
        public Color FillColor { get; protected set; }

        public Shape(IList<ILayout> layouts, EShapeType shapeType,
            Color color = default(Color), Color fillColor = default(Color))
        {
            Layouts = layouts;
            ShapeType = shapeType;
            Color = color;
            FillColor = fillColor;
        }

        public void SetColor(Color color)
        {
            Color = color;
            Layouts.ToList().ForEach(
                layout =>
                {
                    var colorable = (IColorable) layout;
                    if (colorable != null)
                        colorable.SetColor(color);
                });
        }

        public void Fill(Color color)
        {
            FillColor = color;
        }
    }
}