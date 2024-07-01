using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PendulumConfig", menuName = "Configs/Pendulum Config")]
    public class PendulumConfig : ScriptableObject
    {
        [SerializeField, Range(-360, 360)] private float _rotationAngle;
        [SerializeField, Range(1, 10)] private float _rotationSpeed;

        public float RotationAngle => _rotationAngle;
        public float RotationSpeed => _rotationSpeed;
    }
}
