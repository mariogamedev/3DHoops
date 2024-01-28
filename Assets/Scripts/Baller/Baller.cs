using System.Collections.Generic;
using UnityEngine;

namespace Baller
{
    public class Baller : MonoBehaviour
    {
        private Animator _animator;
        private BallHandler _ballHandler;

        private Dictionary<BallerStates,IBallerState> _activeStates = new Dictionary<BallerStates, IBallerState>();

        public Animator Animator => _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            BallerInputNotifications.StartJumpAction += OnStartJump;
            OnIdle();
        }

        private void OnStartJump()
        {
            BallerInputNotifications.EndJumpAction += OnEndJump;
            BallerInputNotifications.StartJumpAction -= OnStartJump;
            AddState(BallerStates.Jump, new FreeJumpState(this));
        }

        private void OnEndJump()
        {
            BallerInputNotifications.EndJumpAction -= OnEndJump;
            BallerInputNotifications.StartJumpAction += OnStartJump;

            RemoveState(BallerStates.Jump);
        }

        private void OnMove(float inputMagnitude, float animationBlend)
        {
            AddState(BallerStates.Move, new MoveState(this, inputMagnitude, animationBlend));
            BallerInputNotifications.MoveAction -= OnMove;
            RemoveState(BallerStates.Idle);
        }

        private void OnIdle()
        {
            AddState(BallerStates.Idle, new IdleState(this));
            BallerInputNotifications.MoveAction += OnMove;
            RemoveState(BallerStates.Move);
        }

        public void OnPickUpBall(Ball ball)
        {
            _ballHandler = new BallHandler(ball);
            AddState(BallerStates.Dribble, new DribbleState(this, _ballHandler));
        }

        public void AddState(BallerStates newStateType, IBallerState newStateInstance)
        {
            if (!_activeStates.ContainsKey(newStateType))
            {
                _activeStates.Add(newStateType, newStateInstance);
                newStateInstance.EnterState();
            }
        }

        public void RemoveState(BallerStates stateToRemove)
        {
            if (_activeStates.ContainsKey(stateToRemove))
            {
                _activeStates[stateToRemove].ExitState();
                _activeStates.Remove(stateToRemove);
            }
        }

        void Update()
        {
            foreach (IBallerState state in _activeStates.Values)
            {
                state.UpdateState();
            }
        }

        void OnDestroy()
        {
            RemoveBallerInputListeners();
        }

        private void RemoveBallerInputListeners()
        {
            BallerInputNotifications.MoveAction -= OnMove;
        }
    }
}