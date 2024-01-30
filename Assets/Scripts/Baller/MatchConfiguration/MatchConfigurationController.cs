using Addresables;
using MVC;

namespace Baller
{
    public class MatchConfigurationController : Controller<MatchConfigurationData>
    {
        MatchConfigurationModel _matchConfigurationModel;
        AddressableSceneLoader _addressableSceneLoader;


        public MatchConfigurationController() 
        { 
            _matchConfigurationModel = new MatchConfigurationModel();
            _addressableSceneLoader = new AddressableSceneLoader();
        }

        protected override void Execute(MatchConfigurationData selectedConfiguration)
        {
            _matchConfigurationModel.SelectedConfiguration = selectedConfiguration;
            _addressableSceneLoader.LoadSceneAsync("Match");
        }
    }
}