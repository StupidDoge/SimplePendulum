using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PendulumConfig", menuName = "Configs/Pendulum Config")]
    public class PendulumConfig : ScriptableObject
    {
        [SerializeField, Range(-179, 179)] private float _rotationAngle;
        [SerializeField, Range(1, 10)] private float _rotationSpeed;
        [SerializeField, Range(1, 20)] private float _dropForce;

        public float RotationAngle => _rotationAngle;
        public float RotationSpeed => _rotationSpeed;
        public float DropForce => _dropForce;
    }
}
