using MVC;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Baller
{
    public class ScoreBoardView : View
    {
        private const string TWO_POINT_SHOOT_TEXT = "2P!";
        private const string THREE_POINT_SHOOT_TEXT = "3P!";
        [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        private TextMeshProUGUI _jumpShootText;

        private int _score = 0;

        private void Start()
        {      
            MatchEventNotifications.ShootScoreAction += OnShootScore;
            MatchEventNotifications.TwoPointShootAtemptAction += OnTwoPointShootAtempt;
            MatchEventNotifications.ThreePointShootAtemptAction += OnThreePointShootAtempt;
        }

        private void OnShootScore(int points)
        {
            _score = points;
            UpdateScoreText();
        }

        private void OnThreePointShootAtempt()
        {
            _jumpShootText.text = THREE_POINT_SHOOT_TEXT;
            EnableJumpShootText();
            StartCoroutine(DisableJumpShootTextAfterDelay());
        }

        private void OnTwoPointShootAtempt()
        {
            _jumpShootText.text = TWO_POINT_SHOOT_TEXT;
            EnableJumpShootText();
            StartCoroutine(DisableJumpShootTextAfterDelay());
        }

        private void EnableJumpShootText()
        {
            _jumpShootText.gameObject.SetActive(true);
        }

        private IEnumerator DisableJumpShootTextAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            DisableJumpShootText();
        }

        private void DisableJumpShootText()
        { 
            _jumpShootText.gameObject.SetActive(false);
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
            MatchEventNotifications.ShootScoreAction -= OnShootScore;
            MatchEventNotifications.TwoPointShootAtemptAction -= OnTwoPointShootAtempt;
            MatchEventNotifications.ThreePointShootAtemptAction -= OnThreePointShootAtempt;
        }
    }
}
