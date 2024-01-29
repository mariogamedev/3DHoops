using MVC;
using UnityEngine;

namespace Baller
{
    [CreateAssetMenu(fileName = "SelectorMenuInstaller", menuName = "Installers/Create SelectorMenu Installer")]
    public class SelectorMenuInstaller : Installer
    {
        public override void Install()
        {
            InstallAction<MatchConfigurationController, StoreMatchConfigurationAction, MatchConfigurationData>();
        }
    }
}