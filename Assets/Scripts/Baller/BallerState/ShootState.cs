
namespace Baller
{
    public class ShootState : BaseBallerState
    {
        public BallHandler _ballHandler;

        public ShootState(Baller baller, BallHandler ballHandler) 
        {
            Baller = baller;
            AnimationTag = "Jump";
            _ballHandler = ballHandler;
        }

        public override void EnterState()
        {
            _ballHandler.Shoot(5f);
        }

        public override void UpdateState()
        {

        }
    }
}