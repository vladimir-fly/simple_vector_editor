using System.Collections.Generic;

namespace SVE.Models.Projects
{
    public class SimpleProject : IProject
    {
        public IList<IShape> Shapes { get; private set; }
        public SimpleProject(IList<IShape> shapes = null)
        {
            Shapes = shapes ?? new List<IShape>();
        }

        public void AddShape(IShape shape)
        {
            Shapes.Add(shape);
        }

        public void RemoveShape(IShape shape)
        {
            if (Shapes.Contains(shape))
                Shapes.Remove(shape);
        }
    }
}