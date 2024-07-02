using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CellsContainer : MonoBehaviour
    {
        public event Action OnAllCellsOccupied;

        [SerializeField] private List<Cell> _cells;

        private void Awake()
        {
            InitializeCellsField();
        }

        private void OnEnable()
        {
            Cell.OnCircleDropped += HandleLogicAfterCircleDrop;
            //Cell.OnLineDestroyed += MoveAllCirclesDown;
        }

        private void OnDisable()
        {
            Cell.OnCircleDropped -= HandleLogicAfterCircleDrop;
            //Cell.OnLineDestroyed -= MoveAllCirclesDown;
        }

        private void InitializeCellsField()
        {
            foreach (Cell cell in _cells)
            {
                cell.TryFindCellAbove();
            }

            foreach (var cell in _cells)
            {
                cell.DisableCellAbove();
            }

            _cells = _cells.OrderBy(x => x.transform.position.y).ToList();
        }

        private void HandleLogicAfterCircleDrop()
        {
            CheckForFullLine();
            CheckIfAllCellsAreOccupied();
        }

        private void CheckForFullLine()
        {
            foreach (var cell in _cells)
            {
                if (cell.InVerticalLine)
                {
                    cell.DeleteVerticalNeighbours();
                    MoveAllCirclesDown();
                    return;
                }
                else if (cell.InHorizontalLine)
                {
                    cell.DeleteHorizontalNeighbours();
                    MoveAllCirclesDown();
                    return;
                }
                else if (cell.InDiagonalLine)
                {
                    cell.DeleteDiagonalNeigbours();
                    MoveAllCirclesDown();
                    return;
                }
            }
        }

        private void MoveAllCirclesDown()
        {
            foreach (var cell in _cells)
            {
                if (cell.HasEmptyCellBelow() && cell.Circle != null)
                {
                    cell.ReleaseCircle();
                }
            }

            StartCoroutine(DisableCellsCoroutine());
        }

        private IEnumerator DisableCellsCoroutine()
        {
            yield return new WaitForSeconds(1f);

            foreach (var cell in _cells)
            {
                if (cell.HasEmptyCellBelow() && cell.Circle == null)
                {
                    cell.gameObject.SetActive(false);
                }
            }
        }

        private void CheckIfAllCellsAreOccupied()
        {
            foreach (var cell in _cells)
            {
                if (cell.transform.childCount == 0)
                {
                    return;
                }
            }

            OnAllCellsOccupied?.Invoke();
        }
    }
}