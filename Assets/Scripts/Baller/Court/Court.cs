using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Baller
{
    public class Court : MonoBehaviour
    {
        [SerializeField]
        private MatchConfiguration _matchConfiguration;
        [SerializeField]
        private Transform _ballerSpawnAnchor;
        [SerializeField]
        private Transform _ballSpawnAnchor;       
        [SerializeField]
        private Transform _ringBoardAnchor;
        [SerializeField]
        private Cinemachine.CinemachineVirtualCamera _virtualCamera;

        private PlayerProximityMediator _playerProximityMediator;

        private List<Baller> _ballers = new List<Baller>();
        private Ball _ball;

        private int _ballerIndex = 0;
        private int _ballIndex = 0;
        private float _threePointDistance;

        public void Start()
        {
            _ballerIndex = Random.Range(0, 2);
            _ballIndex = Random.Range(0, 2);
            _threePointDistance = _matchConfiguration.ThreePointDistance;
            InstantiateBaller();
            InstantiateBall();
            InitilizeProximityMediator();
        }

        private void InstantiateBaller()
        {
            _ballers = new List<Baller>();
            Baller ballerPrefab = _matchConfiguration.LoadBaller(_ballerIndex);
            Baller baller = Instantiate(ballerPrefab, _ballerSpawnAnchor.position, Quaternion.Euler(new Vector3(0, -90f, 0)));
            _ballers.Add(baller);
            _virtualCamera.Follow = baller.transform;
            MatchEventNotifications.JumpShootAction += OnJumpShoot;
        }

        private void InstantiateBall()
        {
            Ball ballPrefab = _matchConfiguration.LoadBall(_ballIndex);
            _ball = Instantiate(ballPrefab, _ballSpawnAnchor.position, Quaternion.identity);
            MatchEventNotifications.ShootClockExhaustedAction += OnShootClockExhausted;
        }

        private void InitilizeProximityMediator()
        {
            _playerProximityMediator = new PlayerProximityMediator(_ball, _ballers.ToArray());
            _playerProximityMediator.IsBallFree = true;
        }

        public void Update()
        {
            _playerProximityMediator.Evaluate();
        }

        private void OnShootClockExhausted()
        {
            MatchEventNotifications.ShootClockExhaustedAction -= OnShootClockExhausted;
            Destroy(_ball.gameObject);
            InstantiateBall();
            InitilizeProximityMediator();
        }

        private void OnJumpShoot()
        {
            float distanceToRingBoard = Vector3.Distance(_ballers[0].transform.position, _ringBoardAnchor.position);

            if (distanceToRingBoard < _threePointDistance)
            {
                MatchEventNotifications.TwoPointShootAtemptAction.Invoke();
            }
            else
            {
                MatchEventNotifications.ThreePointShootAtemptAction.Invoke();
            }
        }
    }
}