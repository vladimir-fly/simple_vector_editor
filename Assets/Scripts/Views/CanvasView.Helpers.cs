using Color = UnityEngine.Color;

namespace SVE.Views
{
    public partial class CanvasView
    {
        private static Models.Color WrapColor(Color color)
        {
            return new Models.Color { a = color.a, r = color.r, g = color.g, b = color.b };
        }

        private static Color WrapColor(Models.Color color)
        {
            return new Color(a: color.a, r: color.r, g: color.g, b:color.b);
        }

    }
}