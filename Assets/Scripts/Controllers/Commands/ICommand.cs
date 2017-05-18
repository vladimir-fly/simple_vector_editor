using SVE.Models;

namespace SVE.Controllers
{
    public interface ICommand
    {
        void Execute();
        void Revert();
    }
}