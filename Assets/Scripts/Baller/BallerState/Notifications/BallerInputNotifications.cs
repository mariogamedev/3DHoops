using System;

namespace Baller
{
    public static class BallerInputNotifications
    {
        public static Action<float,float> MoveAction = delegate { };
        public static Action StartJumpAction = delegate { };
        public static Action EndJumpAction = delegate { };
               
        public static Action<bool> SprintAction = delegate { };
    }
}