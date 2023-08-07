using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BasketJump
{
    public class UIGameover : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _replayBtn;
        [SerializeField] private Button _menuBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _gameoverText;
        [SerializeField] private TextMeshProUGUI _replayBtnText;
        [SerializeField] private TextMeshProUGUI _menuBtnText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _recordText;

        // Cached
        private GameManager _gameManager;
        private string _scoreString = "";
        private string _recordString = "";

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
            _scoreText.text = $"{_scoreString} {_gameManager.Score}";
        }

        private void LoadBest()
        {
            _gameManager.SetBestScore(_gameManager.Score);
            _recordText.text = $"{_recordString} {_gameManager.BestScore}";
        }

        private void LoadLanguague()
        {
            _gameoverText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "GAME OVER");
            _replayBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "REPLAY");
            _menuBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "MENU");
            _scoreString = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SCORE");
            _recordString = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "RECORD");
        }
    }
}
