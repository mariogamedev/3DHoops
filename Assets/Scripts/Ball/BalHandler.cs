using UnityEngine;

namespace Baller
{
    public class BallHandler
    {
        private readonly Ball _ball;
        private readonly Rigidbody _ballRigidbody;

        public BallHandler(Ball ball)
        {
            _ball = ball;
            _ballRigidbody = _ball.GetComponent<Rigidbody>();
            _ballRigidbody.isKinematic = false;
        }

        public void BounceBall(Vector3 throwForce, float playerSpeed)
        {
            Vector3 adjustedForce = throwForce * playerSpeed;
            _ballRigidbody.AddForce(adjustedForce, ForceMode.Impulse);
        }
    }
}