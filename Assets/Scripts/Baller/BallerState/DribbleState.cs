using UnityEngine;

namespace Baller
{
    public class DribbleState : BaseBallerState
    {
        private readonly BallHandler _ballHandler;

        public DribbleState(Baller baller, BallHandler handler)
        {
            Baller = baller;
            AnimationTag = "Dribble";
        }

        public override void UpdateState()
        {
            _ballHandler.BounceBall(new Vector3(0,5,5), 1);
        }
    }
}