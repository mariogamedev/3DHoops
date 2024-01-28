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
    }
}