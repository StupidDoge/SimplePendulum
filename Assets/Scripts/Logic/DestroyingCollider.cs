using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class DestroyingCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
