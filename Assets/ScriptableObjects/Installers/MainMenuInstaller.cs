using MVC;
using UnityEngine;

namespace Baller
{
    [CreateAssetMenu(fileName = "SelectorMenuInstaller", menuName = "Installers/Create SelectorMenu Installer")]
    public class MainMenuInstaller : Installer
    {
        public override void Install()
        {
            InstallAction<MainMenuController, StartGameAction>();
        }
    }
}