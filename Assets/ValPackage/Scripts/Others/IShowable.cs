using System.Threading.Tasks;

namespace ValeryPopov.Common
{
    public interface IShowable
    {
        public Task ShowAsync();
        public Task HideAsync();
        public void HideImmidiatly();
    }
}