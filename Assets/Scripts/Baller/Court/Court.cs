using UnityEngine;

namespace Baller
{
    public class Court : MonoBehaviour
    {
        [SerializeField]
        private Ball _ball;
        [SerializeField]
        private Baller[] _ballers;

        private PlayerProximityMediator _playerProximityMediator;

        public void Awake()
        {
            _playerProximityMediator = new PlayerProximityMediator(_ball, _ballers);
        }

        public void Start()
        {
            _playerProximityMediator.IsBallFree = true;
        }

        public void Update()
        {
            _playerProximityMediator.Evaluate();
        }
    }
}