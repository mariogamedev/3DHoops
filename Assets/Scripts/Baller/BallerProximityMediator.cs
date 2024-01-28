using UnityEngine;

namespace Baller
{
    public class PlayerProximityMediator
    {
        private const float PLAYER_PROXIMITY_THRESHOLD = 2f;
        private readonly Ball _ball;
        private readonly Baller[] _ballers;

        public bool IsBallFree { get; set; }

        public PlayerProximityMediator(Ball ball, Baller[] ballers)
        {
            _ball = ball;
            _ballers = ballers;
        }

        public bool Evaluate()
        {
            if (IsBallFree)
            {
                foreach (Baller baller in _ballers)
                {
                    if (IsPlayerCloseEnough(baller) && IsBallerNearestPlayer(baller))
                    {
                        baller.OnPickUpBall(_ball);
                    }
                }
            }
            
            return false;
        }

        public bool IsBallerNearestPlayer(Baller baller)
        {           
            Baller nearestPlayer = GetNearestPlayer();
            return nearestPlayer == baller;
        }

        private Baller GetNearestPlayer()
        {
            float minDistance = float.MaxValue;
            Baller nearestPlayer = null;

            foreach (Baller baller in _ballers)
            {
                float distanceToPlayer = Vector3.Distance(baller.transform.position, _ball.transform.position);

                if (distanceToPlayer < minDistance)
                {
                    minDistance = distanceToPlayer;
                    nearestPlayer = baller;
                }
            }

            return nearestPlayer;
        }

        public bool IsPlayerCloseEnough(Baller player)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, _ball.transform.position);

            return distanceToPlayer <= PLAYER_PROXIMITY_THRESHOLD;
        }
    }
}