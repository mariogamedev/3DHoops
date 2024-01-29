using UnityEngine;

namespace Baller
{
    public class DribbleState : BaseBallerState
    {
        private const string SECONDARY_ANIMATION_TAG = "DribbleSpeed";
        private const float WALK_SPEED = 1.5f;
        private const float IDLE_SPEED = 2f;
        private const float SPRINT_SPEED = 2.5f;
        private const float SPEED_CHANGE_RATE = 2.0f;
        public const float BOUNCE_FORCE = 0.5f;

        private readonly BallHandler _ballHandler;

        private float _animationBlend;

        public DribbleState(Baller baller, BallHandler handler)
        {
            Baller = baller;
            _ballHandler = handler;
            AnimationTag = "Dribble";
            _animationBlend = WALK_SPEED;
        }

        public override void EnterState()
        {
            base.EnterState();
            Baller.Animator.SetFloat(SECONDARY_ANIMATION_TAG, _animationBlend);
            BallerInputNotifications.SprintAction += OnSprint;
            BallerInputNotifications.MoveAction += OnUpdateInputMove;
        }

        public override void UpdateState()
        {
            Baller.Animator.SetFloat(SECONDARY_ANIMATION_TAG, _animationBlend);
            _ballHandler.UpdateToHand();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            //_ballHandler.BounceBall(Vector3.up * BOUNCE_FORCE);
        }

        public override void ExitState()
        {
            base.ExitState();
            BallerInputNotifications.SprintAction -= OnSprint;
            BallerInputNotifications.MoveAction -= OnUpdateInputMove;
        }

        private void OnUpdateInputMove(float inputMagnitude, float animationBlend)
        {
            if (animationBlend == 0)
            {
                _animationBlend = IDLE_SPEED;
            }
            else if (animationBlend < SPEED_CHANGE_RATE)
            {
                _animationBlend = WALK_SPEED;
            }
            else if (animationBlend > SPEED_CHANGE_RATE)
            {
                _animationBlend = SPRINT_SPEED;
            }            
        }

        private void OnSprint(bool inputSprint)
        {
            float targetSpeed = inputSprint ? SPRINT_SPEED : WALK_SPEED;

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SPEED_CHANGE_RATE);
        }
    }
}