using System.Collections.Generic;
using UnityEngine;

namespace Baller
{
    [CreateAssetMenu(fileName = "MatchConfiguration", menuName = "Match/General", order = 0)]
    public class MatchConfiguration : ScriptableObject
    {
        [SerializeField]
        private BallerSelector _ballerSelectorPrefab;
        [SerializeField]
        private BallSelector _ballSelectorPrefab;
        [SerializeField]
        private Sprite[] _ballerImages;
        [SerializeField]
        private Sprite[] _ballImages;

        [SerializeField]
        private Baller[] _availableBallers;
        [SerializeField]
        private Ball[] _availableBalls;

        [SerializeField]
        private float _threePointDistance;

        public float ThreePointDistance => _threePointDistance;

        public List<BallerSelector> CreateBallers()
        {
            List<BallerSelector> ballers = new List<BallerSelector>();

            for (int i = 0; i < _ballerImages.Length; i++)
            {
                BallerSelector ballerSelector = Instantiate(_ballerSelectorPrefab);
                ballerSelector.Profile.sprite = _ballerImages[i];
                ballerSelector.ID = i;
                ballers.Add(ballerSelector);
            }

            return ballers;
        }

        public List<BallSelector> CreateBalls()
        {
            List<BallSelector> balls = new List<BallSelector>();

            for (int i = 0; i < _ballImages.Length; i++)
            {
                BallSelector ballSelector = Instantiate(_ballSelectorPrefab);
                ballSelector.Profile.sprite = _ballImages[i];
                ballSelector.ID = i;
                balls.Add(ballSelector);
            }

            return balls;
        }

        public Baller LoadBaller(int ballerID)
        {
            foreach(Baller baller in _availableBallers)
            {
                if (baller.ID == ballerID)
                {
                    return baller;
                }
            }

            return new Baller();
        }

        public Ball LoadBall(int ballID)
        {
            foreach (Ball ball in _availableBalls)
            {
                if (ball.ID == ballID)
                {
                    return ball;
                }
            }

            return new Ball();
        }
    }
}