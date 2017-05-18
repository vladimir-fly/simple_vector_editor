using System.Collections.Generic;

namespace SVE.Models
{
    public interface IShape : IColorable, IFillable
    {
        IList<ILayout> Layouts { get; }
        EShapeType ShapeType { get; }
    }
}