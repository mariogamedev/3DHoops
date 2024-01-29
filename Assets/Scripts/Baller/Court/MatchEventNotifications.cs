
using System;

namespace Baller
{
    public static class MatchEventNotifications 
    {
        public static Action PickUpBallAction = delegate { };
        public static Action ShootClockExhaustedAction = delegate { };
        public static Action<int> ShootScore = delegate { };
    }
}