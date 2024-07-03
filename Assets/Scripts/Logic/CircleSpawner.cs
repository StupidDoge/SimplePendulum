using Assets.Scripts.Configs;
using Assets.Scripts.Infrastructure.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CircleSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private List<CircleConfig> _circleConfigs;

        private CirclesFactory _factory;

        public void Init(CirclesFactory factory)
        {
            _factory = factory;
        }

        public Circle SpawnRandomCircle()
        {
            Circle circle = _factory.GetCircleAt(_spawnPoint);
            int randomConfigId = Random.Range(0, _circleConfigs.Count);
            circle.Init(_circleConfigs[randomConfigId]);

            return circle;
        }
    }
}