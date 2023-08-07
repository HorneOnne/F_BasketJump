using UnityEngine;

namespace BasketJump
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private LayerMask _platformLayer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_platformLayer == (_platformLayer | (1 << collision.gameObject.layer)))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
