namespace SVE.Models
{
    public interface IFillable
    {
        Color FillColor { get; }
        void Fill(Color color);
    }
}