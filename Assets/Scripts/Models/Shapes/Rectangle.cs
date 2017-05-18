using System.Collections.Generic;

namespace SVE.Models
{
    public class Rectangle : Shape
    {
        public Rectangle(IList<Straight> layouts, Color color, Color fillColor) :
            base((IList<ILayout>) layouts,color,fillColor)
        {

        }
    }
}