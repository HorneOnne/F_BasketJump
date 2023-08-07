using UnityEngine;
using System.Collections.Generic;

namespace BasketJump
{
    public class GeneratorManager : MonoBehaviour
    {
        public static GeneratorManager Instance { get; private set; }

        [SerializeField] private Generator _generatorPrefab;       
        private Dictionary<Vector2Int, Generator> _generatorDict = new Dictionary<Vector2Int, Generator>();
        [SerializeField] private List<Platform> _platformPrefabs;
        private int _currentPlatformIndex = -1;
        private bool _isLeftToRight = true;

        [Header("Properties")]
        [SerializeField] private Transform _ballPosition;
        private float[] _platformSpeed = new float[3] {2,2,3 };
        private float[] _distanceEachPlatform = new float[3] { 4,4,6};
        

        // Cached
        private Generator _lastestGeneratorSpawned = null;
        private int _distanceEachGenerator = 3;
        private int _rangeToSpawnNewGenerator = 12;
        private float _minGeneratorY = 0.01f;


        // Timer
        private float _updateTimer;
        private float _updateTime = 0.5f;

        private void Awake()
        {
            Instance = this;
        }


        private void OnEnable()
        {
            Generator.OnGeneratorOutOfRange += DestroyGenerator;
        }

      
        private void OnDisable()
        {
            Generator.OnGeneratorOutOfRange -= DestroyGenerator;
        }

        private void Start()
        {
         
        }   


        private void Update()
        {
            if(Time.time - _updateTimer > _updateTime)
            {
                _updateTimer = Time.time;
                if (GameplayManager.Instance.CurrentState == GameplayManager.GameState.GAMEOVER) return;


                int currentY = (int)_ballPosition.position.y;
                int maxY = currentY + _rangeToSpawnNewGenerator;
                for (int value = currentY; value <= maxY; value++)
                {
                    if (value % _distanceEachGenerator != 0) continue;

                    if (_isLeftToRight) 
                    {
                        AddGenerator(new Vector2Int(0, value), Platform.MoveType.LEFT_TO_RIGHT);
                        _isLeftToRight = false;
                    }
                    else
                    {
                        AddGenerator(new Vector2Int(0, value), Platform.MoveType.RIGHT_TO_LEFT);
                        _isLeftToRight = true;
                    }
                    
                }
            }
        }

        private void AddGenerator(Vector2Int position, Platform.MoveType moveType)
        {
            if (position.y % _distanceEachGenerator != 0 || position.y < _minGeneratorY) return;

            if (_generatorDict.ContainsKey(position) == false)
            {
                _lastestGeneratorSpawned = Instantiate(_generatorPrefab, new Vector2(0, position.y), Quaternion.identity);
                _lastestGeneratorSpawned.SetGeneratorProperties(GetNextPlatform(), position, moveType, 
                    Random.Range(_platformSpeed[_currentPlatformIndex] - 1f, _platformSpeed[_currentPlatformIndex] + 1f),
                    Random.Range(_distanceEachPlatform[_currentPlatformIndex] - 0.1f, _distanceEachPlatform[_currentPlatformIndex] + 1.0f));
                _generatorDict.Add(position, _lastestGeneratorSpawned);
            }
        }


        private void DestroyGenerator(Vector2Int position)
        {
            Destroy(_generatorDict[position].gameObject);
            _generatorDict.Remove(position);         
        }

        private Platform GetNextPlatform()
        {
            _currentPlatformIndex = (_currentPlatformIndex + 1) % _platformPrefabs.Count;
            return _platformPrefabs[_currentPlatformIndex];
        }
    }
}
