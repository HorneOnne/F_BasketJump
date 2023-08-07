using UnityEngine;

namespace BasketJump
{
    public class InitPlatform : MonoBehaviour
    {
        public static event System.Action OnFirstJump;
        [SerializeField] private LayerMask _ballLayer;

        private void OnCollisionExit2D(Collision2D collision)
        {           
            if (_ballLayer == (_ballLayer | (1 << collision.gameObject.layer)))
            {
                if (GameplayManager.Instance.CurrentState != GameplayManager.GameState.WAITING)
                {
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PLAYING);
                    OnFirstJump?.Invoke();
                    Destroy(this.gameObject);
                }              
            }
        }
    }
}
