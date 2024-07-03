using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Cell : MonoBehaviour
    {
        public static event Action OnCircleDropped;
        public static event Action OnLineDestroyed;

        private const float CastDistance = 0.01f;

        [SerializeField] private Transform _upCheck;
        [SerializeField] private Transform _upLeftCheck;
        [SerializeField] private Transform _upRightCheck;
        [SerializeField] private Transform _downCheck;
        [SerializeField] private Transform _downLeftCheck;
        [SerializeField] private Transform _downRightCheck;
        [SerializeField] private Transform _leftCheck;
        [SerializeField] private Transform _rightCheck;

        private Cell _aboveCell;
        private BoxCollider2D _boxCollider;

        [field: SerializeField] public Circle Circle { get; private set; }

        public bool InVerticalLine
            => HasSameCircleAt(_upCheck) && HasSameCircleAt(_downCheck);

        public bool InHorizontalLine 
            => HasSameCircleAt(_leftCheck) && HasSameCircleAt(_rightCheck);

        public bool InDiagonalLine
            => InIncreasingDiagonalLine || InDecreasingDiagonalLine;

        private bool InIncreasingDiagonalLine
            => HasSameCircleAt(_upRightCheck) && HasSameCircleAt(_downLeftCheck);

        private bool InDecreasingDiagonalLine
            => HasSameCircleAt(_upLeftCheck) && HasSameCircleAt(_downRightCheck);

        public void TryFindCellAbove()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            Vector2 topPosition = new(transform.position.x, transform.position.y + _boxCollider.bounds.extents.y + CastDistance);
            RaycastHit2D hit = Physics2D.Raycast(topPosition, Vector2.up, CastDistance);

            if (hit.collider != null && hit.transform.TryGetComponent(out Cell cell))
            {
                _aboveCell = cell;
            }
        }

        public bool HasEmptyCellBelow()
        {
            Vector2 checkPosition = new(_downCheck.position.x, _downCheck.position.y);
            RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.zero, CastDistance);

            if (hit.collider != null && hit.transform.TryGetComponent(out Cell cell) && cell.Circle == null)
            {
                return true;
            }

            return false;
        }

        public bool HasSameCircleAt(Transform check)
        {
            Vector2 checkPosition = new(check.position.x, check.position.y);
            RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.zero, CastDistance);

            return HasSameCircleInDirection(hit);
        }

        public Circle GetCircleAt(Transform check)
        {
            Vector2 checkPosition = new(check.position.x, check.position.y);
            RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.zero, CastDistance);

            if (hit.collider != null && hit.collider.TryGetComponent(out Cell cell))
            {
                if (cell.Circle != null) 
                {
                    return cell.Circle;
                }
            }

            throw new NullReferenceException("Circle is null!");
        }

        public bool HasSameCircleInDirection(RaycastHit2D hit)
        {
            if (hit.collider == null)
            {
                return false;
            }

            if (hit.collider.TryGetComponent(out Cell cell))
            {
                if (cell.Circle == null || Circle == null)
                {
                    return false;
                }

                if (Circle.Type == cell.Circle.Type)
                {
                    return true;
                }
            }

            return false;
        }

        public void DeleteHorizontalNeighbours() 
            => DeleteNeighboursAt(_rightCheck, _leftCheck);

        public void DeleteVerticalNeighbours() 
            => DeleteNeighboursAt(_upCheck, _downCheck);

        public void DeleteDiagonalNeigbours()
        {
            if (InIncreasingDiagonalLine)
            {
                DeleteNeighboursAt(_upRightCheck, _downLeftCheck);
            }
            else if (InDecreasingDiagonalLine)
            {
                DeleteNeighboursAt(_upLeftCheck, _downRightCheck);
            }
        }

        public void DisableCellAbove()
        {
            if (_aboveCell != null)
            {
                _aboveCell.gameObject.SetActive(false);
            }
        }

        public void ReleaseCircle()
        {
            Debug.LogError("Release " + gameObject.name);
            //Circle.SetupForDrop();
        }

        private void DeleteNeighboursAt(Transform first, Transform second)
        {
            Circle firstCircle = GetCircleAt(first);
            Circle secondCircle = GetCircleAt(second);

            Destroy(firstCircle.gameObject);
            Destroy(secondCircle.gameObject);
            Destroy(Circle.gameObject);
            Circle = null;

            OnLineDestroyed?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Circle circle))
            {
                Circle = circle;
                Circle.transform.position = transform.position;
                Circle.transform.parent = transform;
                //Circle.SetupForCell();

                if (_aboveCell != null)
                {
                    _aboveCell.gameObject.SetActive(true);
                }

                OnCircleDropped?.Invoke();
            }
        }
    }
}