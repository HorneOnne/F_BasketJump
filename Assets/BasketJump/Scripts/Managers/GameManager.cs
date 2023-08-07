using UnityEngine;

namespace BasketJump
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event System.Action OnScoreUp;

        // SCORE & BEST
        private float _time;
        private float _record;


        #region Properties
        public float Time { get => _time; }
        public float Record { get => _record; }

        #endregion
        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // FPS
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);
        }



        public void ResetTime()
        {
            this._time = 0; 
        }

        public void SetRecord(float record)
        {
            this._time = record;
            if (_record < record)
            {
                _record = record;
            }
        }
    }
}
