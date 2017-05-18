using SVE.Models;

namespace SVE.Controllers
{
    public class Fill : ICommand
    {
        private Color _newColor;
        private Color _previousColor;

        private IFillable _mesh;

        public Fill(IFillable mesh, Color color)
        {
            _mesh = mesh;
            _newColor = color;
            _previousColor = mesh.FillColor;
        }

        public void Execute()
        {
            _mesh.Fill(_newColor);
        }

        public void Revert()
        {
            _mesh.Fill(_previousColor);
        }
    }
}