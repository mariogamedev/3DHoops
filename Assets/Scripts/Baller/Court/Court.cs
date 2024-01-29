using System.Collections.Generic;
using UnityEngine;

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
        private Cinemachine.CinemachineVirtualCamera _virtualCamera;

        private PlayerProximityMediator _playerProximityMediator;

        private List<Baller> _ballers = new List<Baller>();
        private Ball _ball;

        public void Start()
        {
            InstantiateBaller();
            InstantiateBall();
            _playerProximityMediator = new PlayerProximityMediator(_ball, _ballers.ToArray());
            _playerProximityMediator.IsBallFree = true;
        }

        private void InstantiateBaller()
        {
            _ballers = new List<Baller>();
            Baller ballerPrefab = _matchConfiguration.LoadBaller(Random.Range(0, 1));
            Baller baller = Instantiate(ballerPrefab, _ballerSpawnAnchor.position, Quaternion.Euler(new Vector3(0, -90f, 0)));
            _ballers.Add(baller);
            _virtualCamera.Follow = baller.transform;
        }

        private void InstantiateBall()
        {
            Ball ballPrefab = _matchConfiguration.LoadBall(Random.Range(0, 1));
            _ball = Instantiate(ballPrefab, _ballSpawnAnchor.position, Quaternion.identity);
        }

        public void Update()
        {
            _playerProximityMediator.Evaluate();
        }
    }
}