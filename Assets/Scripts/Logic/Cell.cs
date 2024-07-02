using UnityEngine;

namespace Assets.Scripts.Logic
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Cell : MonoBehaviour
    {
        private const float CastOffset = 0.01f;

        private Cell _aboveCell;
        private BoxCollider2D _boxCollider;

        private void Awake()
        {
        }

        public void TryFindCellAbove()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            Vector2 topPosition = new Vector2(transform.position.x, transform.position.y + _boxCollider.bounds.extents.y + CastOffset);
            RaycastHit2D hit = Physics2D.Raycast(topPosition, Vector2.up, CastOffset);

            if (hit.collider != null && hit.transform.TryGetComponent(out Cell cell))
            {
                _aboveCell = cell;
            }
        }

        public void DisableCellAbove()
        {
            if (_aboveCell != null)
            {
                _aboveCell.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Circle circle))
            {
                circle.transform.position = transform.position;
                circle.transform.parent = transform;
                circle.SetupForCell();

                if (_aboveCell != null)
                {
                    _aboveCell.gameObject.SetActive(true);
                }
            }
        }
    }
}