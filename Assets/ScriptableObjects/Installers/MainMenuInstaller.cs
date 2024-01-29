using MVC;
using UnityEngine;

namespace Baller
{
    [CreateAssetMenu(fileName = "MainMenuInstaller", menuName = "Installers/Create MainMenu Installer")]
    public class MainMenuInstaller : Installer
    {
        public override void Install()
        {
            InstallAction<MainMenuController, StartGameAction>();
        }
    }
}