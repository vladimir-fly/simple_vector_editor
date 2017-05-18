using SVE.Models;

namespace SVE.Controllers
{
    public class SetColor : ICommand
    {
        private Color _newColor;
        private Color _previousColor;

        private IColorable _mesh;

        public void Execute(IColorable mesh, Color color)
        {
            _mesh = mesh;
            _newColor = color;
            _previousColor = mesh.Color;
        }

        public void Execute()
        {
            _mesh.SetColor(_newColor);
        }

        public void Revert()
        {
            _mesh.SetColor(_previousColor);
        }
    }
}