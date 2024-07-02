using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CellsContainer : MonoBehaviour
    {
        [SerializeField] private List<Cell> _cells;

        private void Awake()
        {
            foreach (var cell in _cells)
            {
                cell.TryFindCellAbove();
            }

            foreach (var cell in _cells)
            {
                cell.DisableCellAbove();
            }
        }
    }
}