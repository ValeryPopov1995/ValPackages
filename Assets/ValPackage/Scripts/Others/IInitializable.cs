using System.Threading.Tasks;

namespace ValPackage.Common
{
    public interface IInitializable
    {
        bool Initialized { get; }
        Task Initialize();
    }
}