using System.Collections.Generic;

namespace SVE.Models
{
    public interface IProject
    {
        IList<IShape> Shapes { get; }

        void AddShape(IShape shape);
        void RemoveShape(IShape shape);
    }
}