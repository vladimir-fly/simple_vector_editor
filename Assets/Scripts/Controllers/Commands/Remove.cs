using SVE.Models;

namespace SVE.Controllers
{
    public class Remove : ICommand
    {
        private IProject _project;
        private IShape _shape;

        public Remove(IProject project, IShape shape)
        {
            _project = project;
            _shape = shape;
        }

        public void Execute()
        {
            _project.RemoveShape(_shape);
        }

        public void Revert()
        {
            _project.AddShape(_shape);
        }
    }
}