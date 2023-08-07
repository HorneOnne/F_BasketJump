using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BasketJump
{
    public class UIGameover : CustomCanvas
    {
        [Header("Buttons")]       
        [SerializeField] private Button _menuBtn;
        [SerializeField] private Button _replayBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _gameoverText;
        [SerializeField] private TextMeshProUGUI _replayBtnText;
        [SerializeField] private TextMeshProUGUI _menuBtnText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _recordText;

        // Cached
        private GameManager _gameManager;

        private void OnEnable()
        {
            LanguageManager.OnLanguageChanged += LoadLanguague;
            GameplayManager.OnGameOver += LoadScore;
            GameplayManager.OnGameOver += LoadBest;
        }

        private void OnDisable()
        {
            LanguageManager.OnLanguageChanged -= LoadLanguague;
            GameplayManager.OnGameOver -= LoadScore;
            GameplayManager.OnGameOver -= LoadBest;
        }


        private void Start()
        {
            _gameManager = GameManager.Instance;
            LoadScore();
            LoadBest();

            LoadLanguague();

            _replayBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameplayScene);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _menuBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MenuScene);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _replayBtn.onClick.RemoveAllListeners();
            _menuBtn.onClick.RemoveAllListeners();
        }

        private void LoadScore()
        {
            _scoreText.text = $"{TimerManager.Instance.TimeToText()}";
        }

        private void LoadBest()
        {
            _gameManager.SetRecord(TimerManager.Instance.Time);
            _recordText.text = $"{TimerManager.Instance.TimeToText(GameManager.Instance.Record)}";
        }

        private void LoadLanguague()
        {
            if (LanguageManager.Instance.CurrentLanguague == LanguageManager.Languague.France)
            {
                _replayBtnText.fontSize = 30;
                _menuBtnText.fontSize = 30;
            }
            else
            {
                _replayBtnText.fontSize = 35;
                _menuBtnText.fontSize = 35;
            }

            _gameoverText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "GAME OVER");
            _replayBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "REPLAY");
            _menuBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "MENU");
        }
    }
}
