
using UnityEngine;

namespace Baller
{
    public class PickUpBallState : BaseBallerState
    {
        private const float PICKUP_TIMEOUT = 0.6f;
  
        private float _elapsedTime = 0f;
        private Ball _ball;
        private bool _handledBall = false;

        public PickUpBallState(Baller baller, Ball ball) 
        { 
            Baller = baller;
            _ball = ball;
            AnimationTag = "PickUp";
        }

        public override void EnterState()
        {
            base.EnterState();
            MatchEventNotifications.PickUpBallAction.Invoke();
            Reset();
        }

        private void Reset()
        {
            _elapsedTime = 0f;
            _handledBall = false;
        }

        public override void UpdateState()
        {
            if (!_handledBall)
            {
                _ball.EnableKinematic();
                _ball.SetPosition(Baller.HandPosition);
                _handledBall = true;
            }

            if (_elapsedTime >= PICKUP_TIMEOUT)
            {
                BallerInputNotifications.StartDribbleAction.Invoke();
            }

            _elapsedTime += Time.deltaTime;
        }

        public override void ExitState()
        {
            base.ExitState();              
            _ball.DisableKinematic();
        }
    }
}