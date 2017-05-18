using System.Collections.Generic;
using System.Linq;

namespace SVE.Models
{
    public interface ILayout
    {
        IList<Point> Points { get; }
    }
}