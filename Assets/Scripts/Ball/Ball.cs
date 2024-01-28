using UnityEngine;

namespace Baller
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void EnableKinematic()
        {
            _rigidbody.isKinematic = true;
        }

        public void DisableKinematic()
        {
            _rigidbody.isKinematic = false;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}