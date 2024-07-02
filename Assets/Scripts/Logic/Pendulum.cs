using Assets.Scripts.Configs;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Pendulum : MonoBehaviour
    {
        [SerializeField] private CircleSpawner _spawner;
        [SerializeField] private PendulumConfig _pendulumConfig;
        
        private Circle _circle;
        private float _oldRotationAngle;
        private float _movementDirection;
        private float _rotationTime;
        private float _actualDropForce;
        private bool _isReloading;
        private bool _gameIsEnded;

        private bool IsRotatingRight => transform.rotation.z > 0;

        private void Awake()
        {
            _oldRotationAngle = transform.rotation.eulerAngles.z;
            _circle = _spawner.SpawnRandomCircle();
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

            if (Input.GetMouseButtonDown(0) && !_isReloading && !_gameIsEnded)
            {
                _circle.DropWithForce(_actualDropForce, _movementDirection, _pendulumConfig.RotationAngle);
                StartCoroutine(SpawnCircleWithDelay());
            }
        }

        private IEnumerator SpawnCircleWithDelay()
        {
            _isReloading = true;
            yield return new WaitForSeconds(1f);
            _circle = _spawner.SpawnRandomCircle();
            _isReloading = false;
        }

        private void Stop()
        {
            _gameIsEnded = true;
        }
    }
}