using MVC;

namespace Baller
{
    public class MatchConfigurationController : Controller<MatchConfigurationData>
    {
        MatchConfigurationModel _matchConfigurationModel;

        public MatchConfigurationController() 
        { 
            _matchConfigurationModel = new MatchConfigurationModel();
        }

        protected override void Execute(MatchConfigurationData selectedConfiguration)
        {
            _matchConfigurationModel.SelectedConfiguration = selectedConfiguration;
        }
    }
}