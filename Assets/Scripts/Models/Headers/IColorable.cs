namespace SVE.Models
{
    public interface IColorable
    {
        Color Color { get; }
        void SetColor(Color color);
    }
}