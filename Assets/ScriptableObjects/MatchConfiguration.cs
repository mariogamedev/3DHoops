using System.Collections.Generic;
using UnityEngine;

namespace Baller
{
    [CreateAssetMenu(fileName = "MatchConfiguration", menuName = "Match", order = 0)]
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
    }
}