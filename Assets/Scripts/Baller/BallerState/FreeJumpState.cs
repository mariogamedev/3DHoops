using UnityEngine;

namespace Baller
{
    public class FreeJumpState : BaseBallerState
    {
        private const string SECONDARY_ANIMATION_TAG = "FreeFall";
        private const string GROUND_ANIMATION_TAG = "Grounded";
        private const float FALL_TIMEOUT = 0.15f;

        private float _fallTimeoutDelta;

        public FreeJumpState(Baller baller)
        {
            State = BallerStates.Jump;
            Baller = baller;
            AnimationTag = "Jump";
        }

        public override void EnterState()
        {
            base.EnterState();
            Reset();
            Baller.Animator.SetBool(GROUND_ANIMATION_TAG, false);
        }

        private void Reset()
        {
            _fallTimeoutDelta = FALL_TIMEOUT;
        }

        public override void UpdateState()
        {
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                Baller.Animator.SetBool(SECONDARY_ANIMATION_TAG, true);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            Baller.Animator.SetBool(SECONDARY_ANIMATION_TAG, false);
            Baller.Animator.SetBool(GROUND_ANIMATION_TAG, true);
        }
    }
}