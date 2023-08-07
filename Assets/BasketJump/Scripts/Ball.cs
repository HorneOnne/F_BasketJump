using UnityEngine;

namespace BasketJump
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D _rb;

        [Header("JUMP")]
        [SerializeField] private LayerMask _platformLayer;
        [Space(10)]
        [SerializeField] private float _jumpForce = 3f;
        [SerializeField] private float _limitVelocityY = 15;

        // Cached
        private bool _canJump = true;
        private float timer = 0.0f;



        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                if(Utilities.IsPointerOverUIElement() == false)
                    Jump();
            }

            if(Time.time - timer > 0.5f)
            {
                timer = Time.time;
                if (Utilities.IsObjectOutOfCameraView(this.transform))
                {
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
                    SoundManager.Instance.PlaySound(SoundType.GameOver, false);
                    Destroy(this.gameObject);
                }
            }
        }


        private void FixedUpdate()
        {
            if(_rb.velocity.y  > _limitVelocityY)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _limitVelocityY);
            }
            else if(_rb.velocity.y < -_limitVelocityY)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_limitVelocityY);
            }
        }

        private void Jump()
        {
            if(_canJump)
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _canJump = false;
            }
               
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_platformLayer == (_platformLayer | (1 << collision.gameObject.layer)))
            {
                ResetJump();

                SoundManager.Instance.PlaySound(SoundType.Collided, false);
            }
        }

        private void ResetJump()
        {
            _canJump = true;
        }

    }
}
