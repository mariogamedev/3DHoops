using System;
using UnityEngine;

namespace Baller
{
    public static class BallInteractionNotifications
    {
        public static Action<Baller> PickUpBallAction = delegate { };
        public static Action ReleaseResetBallAction = delegate { };
    }
}