using Assets.Scripts.Configs;
using Assets.Scripts.Logic;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Circle _circlePrefab;
    [SerializeField] private List<CircleConfig> _circleConfigs;

    public Circle SpawnRandomCircle()
    {
        int randomConfigId = Random.Range(0, _circleConfigs.Count);

        Circle circle = Instantiate(_circlePrefab, _spawnPoint);
        circle.Init(_circleConfigs[randomConfigId]);

        return circle;
    }
}
