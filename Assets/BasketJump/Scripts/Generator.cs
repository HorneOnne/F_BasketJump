using UnityEngine;
using System.Collections.Generic;

namespace BasketJump
{
    public class Generator : MonoBehaviour
    {
        public static event System.Action<Vector2Int> OnGeneratorOutOfRange;

        [Header("References")]
        [SerializeField] private Transform _leftSpawnPos;
        [SerializeField] private Transform _rightSpawnPos;
  

        [Header("Properties")]
        public float _distanceEachPlatform = 3;
        public float _platformSpeed = 3f;

        // Cached
        private Platform _platformPrefab;
        private Platform _lastestObjectSpawned = null;
        private Platform.MoveType _moveType;
        private bool _isSetProperties = false;
        private Transform _camPosition;
        private float _destroyOffsetY = 5.5f;

        private float _destroyTimer = 0.0f;

        #region Properties
        [field: SerializeField] public Vector2Int Position { get; private set; }
        #endregion

        private void Awake()
        {
            _camPosition = Camera.main.transform;
        }

  

        private void Update()
        {
            if (_isSetProperties == false) return;
            if (GameplayManager.Instance.CurrentState == GameplayManager.GameState.GAMEOVER) return;

            if (_lastestObjectSpawned == null)
            {
                if (_moveType == Platform.MoveType.LEFT_TO_RIGHT)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 offsetPosition = new Vector2((_leftSpawnPos.position.x + (i + 1) * _distanceEachPlatform), _leftSpawnPos.position.y);
                        SpawnPlatform(_platformPrefab, offsetPosition).SetPlatformProperties(_leftSpawnPos.position, _moveType, _platformSpeed);
                    }
                }
                else if (_moveType == Platform.MoveType.RIGHT_TO_LEFT)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 offsetPosition = new Vector2((_rightSpawnPos.position.x - (i + 1) * _distanceEachPlatform), _rightSpawnPos.position.y);
                        SpawnPlatform(_platformPrefab, offsetPosition).SetPlatformProperties(_rightSpawnPos.position, _moveType, _platformSpeed);
                    }
                }
            }                
            else
            {
                if (_lastestObjectSpawned.GetMoveDistance() <= _distanceEachPlatform) return;
                switch (_moveType)
                {
                    default: break;
                    case Platform.MoveType.LEFT_TO_RIGHT:
                        SpawnPlatform(_platformPrefab,_leftSpawnPos.position).SetPlatformProperties(_leftSpawnPos.position, _moveType, _platformSpeed);
                        break;
                    case Platform.MoveType.RIGHT_TO_LEFT:
                        SpawnPlatform(_platformPrefab, _rightSpawnPos.position).SetPlatformProperties(_rightSpawnPos.position, _moveType, _platformSpeed);
                        break;
                }
            }     
            

            // Destroy when out of range.
            if(Time.time - _destroyTimer > 1.0f)
            {
                _destroyTimer = Time.time;
                if (transform.position.y + _destroyOffsetY < _camPosition.position.y)
                {
                    OnGeneratorOutOfRange?.Invoke(Position);
                }
            }   
        }

        private Platform SpawnPlatform(Platform platform, Vector2 position)
        { 
            _lastestObjectSpawned = Instantiate(platform, position, Quaternion.identity);
            return _lastestObjectSpawned;
        }


        public void SetGeneratorProperties(Platform platformPrefab, Vector2Int position, Platform.MoveType moveType, float platformSpeed, float distanceEachPlatform)
        {
            this.Position = position;
            this._moveType = moveType;
            this._platformPrefab = platformPrefab;
            this._platformSpeed= platformSpeed;
            this._distanceEachPlatform = distanceEachPlatform;

            _isSetProperties = true;
        }
    }
}
