using System.Threading.Tasks;

namespace ValeryPopov.Common
{
    public interface IInitializable
    {
        bool Initialized { get; }
        Task Initialize();
    }
}