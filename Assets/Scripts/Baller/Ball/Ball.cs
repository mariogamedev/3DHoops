using UnityEngine;

namespace Baller
{
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private int _id;

        private Rigidbody _rigidbody;

        public int ID => _id;

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