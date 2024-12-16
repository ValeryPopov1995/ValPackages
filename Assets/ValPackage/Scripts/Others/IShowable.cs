using System.Threading.Tasks;

namespace ValPackage.Common
{
    public interface IShowable
    {
        public Task ShowAsync();
        public Task HideAsync();
        public void HideImmidiatly();
    }
}