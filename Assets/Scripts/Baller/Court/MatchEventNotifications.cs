
using System;

namespace Baller
{
    public static class MatchEventNotifications 
    {
        public static Action PickUpBallAction = delegate { };
        public static Action ShootClockExhaustedAction = delegate { };
        public static Action<int> ShootScoreAction = delegate { };
        public static Action JumpShootAction = delegate { };
        public static Action TwoPointShootAtemptAction = delegate { };
        public static Action ThreePointShootAtemptAction = delegate { };
    }
}