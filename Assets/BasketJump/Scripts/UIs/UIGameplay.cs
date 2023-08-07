using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BasketJump
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _pauseBtn;


        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _timeText;


        // Cached
        private TimerManager _timerMnager;
        private float _updateTimerFrequence = 0.2f;
        private float _updateTimerFrequenceCount = 0.0f;

        private void Awake()
        {
            _timerMnager = TimerManager.Instance;
        }

        private void OnEnable()
        {
            GameManager.OnScoreUp += UpdateTimeUI;
        }

        private void OnDisable()
        {
            GameManager.OnScoreUp -= UpdateTimeUI;
        }


        private void Start()
        {
            UpdateTimeUI();


            _pauseBtn.onClick.AddListener(() =>
            {
                UIGameplayManager.Instance.CloseAll();
                UIGameplayManager.Instance.DisplayPauseMenu(true);
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PAUSE);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

        }


        private void Update()
        {
            if (Time.time - _updateTimerFrequenceCount > _updateTimerFrequence)
            {
                _updateTimerFrequenceCount = Time.time;
                UpdateTimeUI();
            }
        }
    
        private void UpdateTimeUI()
        {
            _timeText.text = _timerMnager.TimeToText();
        }

        private void OnDestroy()
        {
            _pauseBtn.onClick.RemoveAllListeners();
        }

    }
}
