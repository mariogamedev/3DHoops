using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baller
{
    public class Baller : MonoBehaviour
    {
        [SerializeField]
        private Transform _handDribble;
        [SerializeField] 
        private int _id;

        private Animator _animator;
        private BallHandler _ballHandler;

        private Dictionary<BallerStates,IBallerState> _activeStates = new Dictionary<BallerStates, IBallerState>();

        public Animator Animator => _animator;
        public int ID => _id;
        public Vector3 HandPosition { get { return _handDribble.position; } }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            BallerInputNotifications.StartJumpAction += OnStartFreeJump;
            OnIdle();
        }

        private void OnStartFreeJump()
        {
            BallerInputNotifications.StartJumpAction -= OnStartFreeJump;
            BallerInputNotifications.LandAction += OnLand;
            AddState(BallerStates.Jump, new FreeJumpState(this));
        }

        private void OnStartJumpShoot()
        {
            BallerInputNotifications.StartJumpAction -= OnStartJumpShoot;
            RemoveState(BallerStates.Dribble);
            AddState(BallerStates.Jump, new ShootState(this, _ballHandler));
        }

        private void OnLand()
        {
            BallerInputNotifications.LandAction -= OnLand;

            if (_activeStates.ContainsKey(BallerStates.Dribble))
            {
                BallerInputNotifications.StartJumpAction += OnStartJumpShoot;
            }
            else
            {
                BallerInputNotifications.StartJumpAction += OnStartFreeJump;
            }

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

        public void FaultDropBall()
        {
            RemoveState(BallerStates.Dribble);
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