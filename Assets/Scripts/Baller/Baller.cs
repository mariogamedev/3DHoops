using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baller
{
    public class Baller : MonoBehaviour
    {
        [SerializeField]
        private Transform _handDribble;

        private Animator _animator;
        private BallHandler _ballHandler;

        private Dictionary<BallerStates,IBallerState> _activeStates = new Dictionary<BallerStates, IBallerState>();

        public Animator Animator => _animator;
        public Vector3 HandPosition { get { return _handDribble.position; } }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            BallerInputNotifications.StartJumpAction += OnStartJump;
            OnIdle();
        }

        private void OnStartJump()
        {
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

        public void PickUpBall(Ball ball)
        {
            _ballHandler = new BallHandler(ball, _handDribble);
            BallerInputNotifications.StartDribbleAction += OnDribble;
            AddState(BallerStates.PickUpBall, new PickUpBallState(this, ball));
        }

        private void OnDribble()
        {
            StartCoroutine(OnDribbleDelay());
        }
        
        private IEnumerator OnDribbleDelay()
        {
            yield return new WaitForEndOfFrame();
            AddState(BallerStates.Dribble, new DribbleState(this, _ballHandler));
            RemoveState(BallerStates.PickUpBall);
        }

        public void AddState(BallerStates newStateType, IBallerState newStateInstance)
        {
            Debug.Log("Adding state: " + newStateType);
            if (!_activeStates.ContainsKey(newStateType))
            {
                _activeStates.Add(newStateType, newStateInstance);
                newStateInstance.EnterState();
            }
        }

        public void RemoveState(BallerStates stateToRemove)
        {
            Debug.Log("Removing state: " + stateToRemove);
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

        void FixedUpdate()
        {
            foreach (IBallerState state in _activeStates.Values)
            {
                state.FixedUpdateState();
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