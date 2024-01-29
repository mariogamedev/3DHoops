using MVC;
using TMPro;
using UnityEngine;

namespace Baller
{
    public class ScoreBoardView : View
    {
        [SerializeField]
        private TextMeshProUGUI _scoreText;

        private int _score = 0;

        private void Start()
        {
            MatchEventNotifications.ShootScore += OnShootScore;
        }

        private void OnShootScore(int points)
        {
            _score = points;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            _scoreText.text = _score.ToString();
        }

        private void Reset()
        {
            _score = 0;
            UpdateScoreText();
        }

        private void OnDestroy()
        {
            MatchEventNotifications.ShootScore -= OnShootScore;
        }
    }
}
