using UnityEngine;

namespace Baller
{
    public class MoveState : BaseBallerState
    {
        private const string SECONDARY_ANIMATION_TAG = "MotionSpeed";
        private const float MOVE_SPEED = 2.0f;
        private const float SPRINT_SPEED = 5.335f;
        private const float SPEED_CHANGE_RATE = 10.0f;

        private float _inputMagnitude;   
        private float _animationBlend;

        public MoveState(Baller baller, float inputMagnitude, float animationBlend)
        {
            State = BallerStates.Move;
            Baller = baller;
            AnimationTag = "Speed";
            _inputMagnitude = inputMagnitude;
            _animationBlend = animationBlend;
        }

        public override void EnterState()
        {
            Baller.Animator.SetFloat(AnimationTag, _animationBlend);
            Baller.Animator.SetFloat(SECONDARY_ANIMATION_TAG, _inputMagnitude);
            BallerInputNotifications.SprintAction += OnSprint;
            BallerInputNotifications.MoveAction += OnUpdateInputMove;
        }

        public override void UpdateState()
        {
            Baller.Animator.SetFloat(AnimationTag, _animationBlend);
        }

        public override void ExitState()
        {
            base.ExitState();
            BallerInputNotifications.SprintAction -= OnSprint;
            BallerInputNotifications.MoveAction -= OnUpdateInputMove;
        }

        private void OnUpdateInputMove(float inputMagnitude, float animationBlend)
        {
            _inputMagnitude = inputMagnitude;
            _animationBlend = animationBlend;
            Baller.Animator.SetFloat(AnimationTag, _animationBlend);
            Baller.Animator.SetFloat(SECONDARY_ANIMATION_TAG, _inputMagnitude);
        }

        private void OnSprint(bool inputSprint)
        {
            float targetSpeed = inputSprint ? SPRINT_SPEED : MOVE_SPEED;

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SPEED_CHANGE_RATE);
        }
    }
}
