using System.Collections.Generic;

namespace SVE.Models
{
    public class Cirle : Shape
    {
        public Cirle(IList<Curve> layouts, Color color, Color fillColor) :
            base((IList<ILayout>) layouts, color, fillColor)
        {

        }
    }
}