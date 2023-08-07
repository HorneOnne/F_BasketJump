using UnityEngine;

namespace BasketJump
{
    public class Platform : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Generator _generator;
        private Vector2 _initPosition;
        private float _moveSpeed;
        private bool _isSetProperties = false;

        private const float DESTROY_DISTANCE = 30f;

        public enum MoveType
        {
            RIGHT_TO_LEFT,
            LEFT_TO_RIGHT,
        }

        public MoveType Movetype;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetPlatformProperties(Vector2 initPosition, MoveType moveType, float moveSpeed)
        {
            _initPosition = initPosition;
            this.Movetype = moveType;
            this._moveSpeed = moveSpeed;
            _isSetProperties = true;
        }

        private void FixedUpdate()
        {
            if (_isSetProperties == false) return;

            switch(Movetype)
            {
                default: break;
                case MoveType.RIGHT_TO_LEFT:
                    _rb.MovePosition(_rb.position + Vector2.left * _moveSpeed * Time.fixedDeltaTime);
                    break;
                case MoveType.LEFT_TO_RIGHT:
                    _rb.MovePosition(_rb.position + Vector2.right * _moveSpeed * Time.fixedDeltaTime);
                    break;
            }
        

            if (GetMoveDistance() > DESTROY_DISTANCE)
            {
                Destroy(this.gameObject);
            }
        }


        public float GetMoveDistance()
        {
            return Vector2.Distance(_initPosition, transform.position);
        }

  
    }
}
