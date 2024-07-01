using Assets.Scripts.Configs;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Pendulum : MonoBehaviour
    {
        [SerializeField] private PendulumConfig _pendulumConfig;
        [SerializeField] private Rigidbody2D _circle;

        private float _rotationTime;
        private float _actualDropForce;

        private bool IsRotatingRight => transform.rotation.z > 0;

        private void Update()
        {
            CalculateRotation();
            CalculateDropForce();
        }

        private void CalculateRotation()
        {
            float rotationAngle = _pendulumConfig.RotationAngle * Mathf.Sin(_rotationTime * _pendulumConfig.RotationSpeed);
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
            _rotationTime += Time.deltaTime;
        }

        private void CalculateDropForce()
        {
            float forceMultiplier = IsRotatingRight ? 
                Mathf.InverseLerp(.5f, 0, transform.rotation.z) : 
                Mathf.InverseLerp(-.5f, 0, transform.rotation.z);
            _actualDropForce = _pendulumConfig.DropForce * forceMultiplier;

            if (Input.GetMouseButtonDown(0))
            {
                DropCircleWithForce(_actualDropForce);
            }
        }

        private void DropCircleWithForce(float force)
        {
            _circle.transform.parent = null;
            _circle.isKinematic = false;

            float angle = transform.rotation.eulerAngles.z;
            if (angle > 180)
            {
                angle -= 360;
            }

            float dropDirectionFromAngle = angle / _pendulumConfig.RotationAngle;

            _circle.AddForce(force * transform.right * dropDirectionFromAngle, ForceMode2D.Impulse);
        }
    }
}