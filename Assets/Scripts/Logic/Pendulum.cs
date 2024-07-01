using Assets.Scripts.Configs;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Pendulum : MonoBehaviour
    {
        [SerializeField] private PendulumConfig _pendulumConfig;

        private float _rotationTime;

        private void Update() 
            => CalculateRotation();

        private void CalculateRotation()
        {
            _rotationTime += Time.deltaTime;
            float rotationAngle = _pendulumConfig.RotationAngle * Mathf.Sin(_rotationTime * _pendulumConfig.RotationSpeed);
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }
}