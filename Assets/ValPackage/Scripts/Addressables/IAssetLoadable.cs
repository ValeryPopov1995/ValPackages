using System.Threading.Tasks;

namespace ValeryPopov.Common.Addressables
{
    public interface IAssetLoadable
    {
        public Task OnInstantiate();
        public Task OnRelease();
    }
}