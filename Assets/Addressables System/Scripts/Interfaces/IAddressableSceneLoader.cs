

namespace Addresables
{
    public interface IAddressableSceneLoader
    {
        public void LoadSceneAsync(object key, bool activateOnLoad = true, int priority = 100);
    }
}
