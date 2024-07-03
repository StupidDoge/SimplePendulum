using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CircleVisualEffect : MonoBehaviour
{
    [SerializeField] private float _waitingTime = 0.5f;

    private ParticleSystem _particleSystem;

    public float WaitingTime => _waitingTime;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        _particleSystem.Play();
    }
}
