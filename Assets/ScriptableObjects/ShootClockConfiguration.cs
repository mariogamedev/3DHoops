using UnityEngine;

namespace Baller
{
    [CreateAssetMenu(fileName = "ShootClockConfiguration", menuName = "Match/ShootClock", order = 0)]
    public class ShootClockConfiguration : ScriptableObject
    {
        [SerializeField]
        private int _countdownStartTime;
        [SerializeField]
        private Sprite _timerSprite;

        public int CountdownStartTime => _countdownStartTime;
        public Sprite TimerSprite => _timerSprite;
    }
}