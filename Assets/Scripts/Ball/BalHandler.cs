using UnityEngine;

namespace Baller
{
    public class BallHandler
    {
        private readonly Ball _ball;
        private readonly Rigidbody _ballRigidbody;
        private readonly Transform _hand;

        public BallHandler(Ball ball, Transform handHandler)
        {
            _ball = ball;
            _hand = handHandler;
            _ballRigidbody = _ball.GetComponent<Rigidbody>();
            _ballRigidbody.isKinematic = false;
        }

        public void BounceBall(Vector3 force)
        {
            _ballRigidbody.AddForce(force, ForceMode.Impulse);
        }

        public void UpdateToHand()
        {
            _ball.transform.position = _hand.position;
        }


    }
}