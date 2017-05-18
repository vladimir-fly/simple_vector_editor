using System.Collections.Generic;

namespace SVE.Models
{
    public interface IShape
    {
        IList<ILayout> Layouts { get; }
    }
}