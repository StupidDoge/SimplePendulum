using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "CircileConfig", menuName = "Configs/Circle Config")]
    public class CircleConfig : ScriptableObject
    {
        [SerializeField] private Color _color;
        [SerializeField, Min(0)] private int _score;

        public Color Color => _color;
        public int Score => _score;
    }
}