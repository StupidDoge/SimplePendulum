using UnityEngine;

namespace Assets.Scripts.Logic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Circle : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}