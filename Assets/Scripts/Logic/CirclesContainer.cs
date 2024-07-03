using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CirclesContainer : MonoBehaviour
    {
        [SerializeField] private DestroyingCollider _destroyingCollider;
        [SerializeField] private float _colliderEnableDelay;

        [SerializeField] private int _circlesInContainer;

        public bool IsFull => _destroyingCollider.gameObject.activeInHierarchy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _circlesInContainer++;

            if (_circlesInContainer == 3)
            {
                StartCoroutine(EnableDestroyingColliderWithDelay());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _circlesInContainer--;

            if (_circlesInContainer != 3)
            {
                _destroyingCollider.gameObject.SetActive(false);
                StopAllCoroutines();
            }
        }

        private IEnumerator EnableDestroyingColliderWithDelay()
        {
            yield return new WaitForSeconds(_colliderEnableDelay);

            _destroyingCollider.gameObject.SetActive(true);
        }
    }
}
