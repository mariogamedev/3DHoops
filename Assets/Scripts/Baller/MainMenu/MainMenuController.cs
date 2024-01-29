using MVC;
using UnityEngine.SceneManagement;

namespace Baller
{
    public class MainMenuController : Controller
    {
        protected override void Execute()
        {
            SceneManager.LoadScene("SelectionMenu");
        }
    }
}