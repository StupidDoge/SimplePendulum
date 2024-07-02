using Assets.Scripts.Configs;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Circle : MonoBehaviour
    {
        private CircleConfig _circleConfig;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _sprite;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void Init(CircleConfig config)
        {
            _circleConfig = config;
            _sprite.color = config.Color;
        }

        public void DropWithForce(float force, float pendulumMovementDirection, float pendulumRotationAngle)
        {
            transform.parent = null;
            _rigidbody.isKinematic = false;

            int directionFromMovement = pendulumMovementDirection > 0 ? 1 : -1;
            float angle = transform.rotation.eulerAngles.z;
            if (angle > 180)
            {
                angle -= 360;
                directionFromMovement *= -1;
            }

            float dropDirection = angle / pendulumRotationAngle * directionFromMovement;

            _rigidbody.AddForce(force * dropDirection * transform.right, ForceMode2D.Impulse);
        }

        public void SetupForCell()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.simulated = false;
        }
    }
}