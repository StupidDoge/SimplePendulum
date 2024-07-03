using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class CirclesFactory : MonoBehaviour
    {
        [SerializeField] private Circle _circlePrefab;

        public Circle GetCircleAt(Transform at) 
            => Instantiate(_circlePrefab, at);
    }
}