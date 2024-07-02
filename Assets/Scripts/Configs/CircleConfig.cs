using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "CircileConfig", menuName = "Configs/Circle Config")]
    public class CircleConfig : ScriptableObject
    {
        public enum CircleType
        {
            Red = 0,
            Blue = 1,
            Green = 2
        }

        [SerializeField] private CircleType _type;
        [SerializeField] private Color _color;
        [SerializeField, Min(0)] private int _score;

        public CircleType Type => _type;
        public Color Color => _color;
        public int Score => _score;
    }
}