using MVC;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Baller
{
    public class ShotClockView : View
    {
        [SerializeField]
        private ShootClockConfiguration _shootClockConfiguration;
        [SerializeField]
        private TextMeshProUGUI _timer;
        [SerializeField]
        private Image _timerImage;

        private float _elapsedTime = 0f;
        private float _startTimeCountdown = 0f;

        private bool _isShotClockOn = false;

        private void Start()
        {
            _startTimeCountdown = _shootClockConfiguration.CountdownStartTime;
            _timerImage.sprite = _shootClockConfiguration.TimerSprite;
            MatchEventNotifications.PickUpBallAction += OnStartShootClock;
            Reset();
        }

        private void Update()
        {
            if (_isShotClockOn)
            {
                RunClock();
            }
        }

        private void RunClock()
        {
            if (_elapsedTime <= 0f)
            {
                _elapsedTime = 0f;
                ShootClockExhausted();
            }
            else
            {
                _elapsedTime -= Time.deltaTime;
            }

            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            _timer.text = Mathf.FloorToInt(_elapsedTime).ToString();
        }

        private void Reset()
        {
            _isShotClockOn = false;
            _elapsedTime = _startTimeCountdown;
            UpdateTimerText();
        }

        private void OnStartShootClock()
        {
            _isShotClockOn = true;
        }

        private void ShootClockExhausted()
        {
            _isShotClockOn = false;
            MatchEventNotifications.ShootClockExhaustedAction.Invoke();
            StartCoroutine(ResetClockDelay());
        }

        private IEnumerator ResetClockDelay()
        {
            yield return new WaitForSeconds(0.5f);
            Reset();
        }

        private void OnResetClock()
        {
            Reset();
        }
    }
}
