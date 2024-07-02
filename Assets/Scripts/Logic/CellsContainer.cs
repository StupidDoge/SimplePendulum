using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CellsContainer : MonoBehaviour
    {
        public event Action OnAllCellsOccupied;

        [SerializeField] private List<Cell> _cells;

        private void Awake()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                Cell cell = _cells[i];
                cell.Id = i;
                cell.TryFindCellAbove();
            }

            foreach (var cell in _cells)
            {
                cell.DisableCellAbove();
            }
        }

        private void OnEnable()
        {
            Cell.OnCircleDropped += HandleLogic;
        }

        private void OnDisable()
        {
            Cell.OnCircleDropped -= HandleLogic;
        }

        private void HandleLogic()
        {
            CheckForLine();
            CheckIfAllSellsIsOccupied();
        }

        private void CheckForLine()
        {
            foreach (var cell in _cells)
            {
                if (cell.InVerticalLine)
                {
                    Debug.Log("vertical");
                }
                else if (cell.InHorizontalLine)
                {
                    Debug.Log("horizontal");
                }
                else if (cell.InDiagonalLine)
                {
                    Debug.Log("diagonal");
                }
            }
        }

        private void CheckIfAllSellsIsOccupied()
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