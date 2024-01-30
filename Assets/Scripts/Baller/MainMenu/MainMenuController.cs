using Addresables;
using MVC;

namespace Baller
{
    public class MainMenuController : Controller
    {
        AddressableSceneLoader _addressableSceneLoader;

        public MainMenuController()
        {
            _addressableSceneLoader = new AddressableSceneLoader();
        }

        protected override void Execute()
        {
            _addressableSceneLoader.LoadSceneAsync("SelectionMenu");
        }
    }
}