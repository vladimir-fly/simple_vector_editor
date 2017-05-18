using SVE.Models;

namespace SVE.Controllers
{
    public class Add : ICommand
    {
        private IProject _project;
        private IShape _shape;

        public Add(IProject project, IShape shape)
        {
            _project = project;
            _shape = shape;
        }

        public void Execute()
        {
            _project.AddShape(_shape);
        }

        public void Revert()
        {
            _project.RemoveShape(_shape);
        }
    }
}