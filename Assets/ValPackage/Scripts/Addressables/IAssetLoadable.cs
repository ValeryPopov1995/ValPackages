using System.Threading.Tasks;

namespace ValPackage.Common.Addressables
{
    public interface IAssetLoadable
    {
        public Task OnInstantiate();
        public Task OnRelease();
    }
}