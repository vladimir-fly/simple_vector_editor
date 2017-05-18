using System.Collections.Generic;
using System.Linq;

namespace SVE.Models
{
    public class Shape : IShape, IColorable, IFillable
    {
        public IList<ILayout> Layouts { get; protected set; }
        public Color Color { get; protected set; }
        public Color FillColor { get; protected set; }

        public Shape(IList<ILayout> layouts, Color color, Color fillColor)
        {
            Layouts = layouts;
            Color = color;
            FillColor = fillColor;
        }

        public void SetColor(Color color)
        {
            Color = color;
            Layouts.ToList().ForEach(
                layout =>
                {
                    var colorable = layout as IColorable;
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