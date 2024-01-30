
using System.Diagnostics;

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
            base.EnterState();
            MatchEventNotifications.JumpShootAction.Invoke();
            _ballHandler.Shoot(1f);
        }

        public override void UpdateState()
        {

        }
    }
}