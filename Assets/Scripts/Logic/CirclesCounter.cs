using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CirclesCounter : MonoBehaviour
    {
        public event Action OnFieldFullfilled;

        private const float ContainersCheckDelay = 1f;

        [SerializeField] private List<CirclesContainer> _circlesContainers;
        [SerializeField] private List<Circle> _circles;

        private void OnEnable()
        {
            Circle.OnCircleDropped += CheckForRemoveableLines;
            Circle.OnLineDestroyed += RemoveCirclesFromList;
        }

        private void OnDisable()
        {
            Circle.OnCircleDropped -= CheckForRemoveableLines;
            Circle.OnLineDestroyed -= RemoveCirclesFromList;
        }

        private void CheckForRemoveableLines(Circle circle)
        {
            if (!_circles.Contains(circle))
            {
                _circles.Add(circle);
            }

            foreach (var item in _circles)
            {
                if (item.InVerticalLine)
                {
                    item.DeleteVerticalNeighbours();
                    return;
                }
                else if (item.InHorizontalLine)
                {
                    item.DeleteHorizontalNeighbours();
                    return;
                }
                else if (item.InDiagonalLine)
                {
                    item.DeleteDiagonalNeigbours();
                    return;
                }
            }

            StartCoroutine(CheckIfContainersAreFullCoroutine());
        }

        private void RemoveCirclesFromList() 
            => StartCoroutine(RemoveCircleFromListCoroutine());

        private IEnumerator RemoveCircleFromListCoroutine()
        {
            yield return new WaitForEndOfFrame();

            _circles.RemoveAll(circle => circle == null);
        }

        private IEnumerator CheckIfContainersAreFullCoroutine()
        {
            yield return new WaitForSeconds(ContainersCheckDelay);

            if (_circlesContainers.All(container => container.IsFull))
            {
                OnFieldFullfilled?.Invoke();
            }
        }
    }
}