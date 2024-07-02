using Assets.Scripts.Configs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Logic
{
    public class Pendulum : MonoBehaviour
    {
        [SerializeField] private PendulumConfig _pendulumConfig;
        [SerializeField] private Rigidbody2D _circle;

        private float _oldRotationAngle;
        private float _movementDirection;

        private float _rotationTime;
        private float _actualDropForce;

        private bool IsRotatingRight => transform.rotation.z > 0;

        private void Awake()
        {
            _oldRotationAngle = transform.rotation.eulerAngles.z;
        }

        private void Update()
        {
            CalculateRotation();
            CalculateDropForce();
        }

        private void CalculateRotation()
        {
            float angle = _pendulumConfig.RotationAngle * Mathf.Sin(_rotationTime * _pendulumConfig.RotationSpeed);
            _oldRotationAngle = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            _movementDirection = transform.rotation.eulerAngles.z - _oldRotationAngle;
            _rotationTime += Time.deltaTime;
        }

        private void CalculateDropForce()
        {
            float forceMultiplier = IsRotatingRight ? 
                Mathf.InverseLerp(.5f, 0, transform.rotation.z) : 
                Mathf.InverseLerp(-.5f, 0, transform.rotation.z);
            _actualDropForce = _pendulumConfig.DropForce * forceMultiplier * _pendulumConfig.RotationSpeed;

            if (Input.GetMouseButtonDown(0))
            {
                DropCircleWithForce(_actualDropForce);
            }
        }

        private void DropCircleWithForce(float force)
        {
            _circle.transform.parent = null;
            _circle.isKinematic = false;

            int directionFromMovement = _movementDirection > 0 ? 1 : -1;
            float angle = transform.rotation.eulerAngles.z;
            if (angle > 180)
            {
                angle -= 360;
                directionFromMovement *= -1;
            }

            float dropDirection = angle / _pendulumConfig.RotationAngle * directionFromMovement;

            _circle.AddForce(force * dropDirection * transform.right, ForceMode2D.Impulse);
        }
    }
}