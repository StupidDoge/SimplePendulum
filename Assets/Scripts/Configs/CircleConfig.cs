using UnityEngine;

[CreateAssetMenu(fileName = "CircileConfig", menuName = "Configs/Circle Config")]
public class CircleConfig : ScriptableObject
{
    [SerializeField] private Color _color;
    [SerializeField] private int _score;

    public Color Color => _color;
    public int Score => _score;
}
