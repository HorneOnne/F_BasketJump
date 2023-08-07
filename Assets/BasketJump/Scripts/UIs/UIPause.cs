using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BasketJump
{
    public class UIPause : CustomCanvas
    {
        [Header("Buttons")]     
        [SerializeField] private Button _menuBtn;
        [SerializeField] private Button _backBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _pauseHeadingText;
        [SerializeField] private TextMeshProUGUI _menuBtnText;
        [SerializeField] private TextMeshProUGUI _backBtnText;

        private void OnEnable()
        {
            LanguageManager.OnLanguageChanged += LoadLanguague;
        }

        private void OnDisable()
        {
            LanguageManager.OnLanguageChanged -= LoadLanguague;
        }


        private void Start()
        {
            LoadLanguague();

            _backBtn.onClick.AddListener(() =>
            {
                UIGameplayManager.Instance.CloseAll();
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PLAYING);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _menuBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1.0f;
                Loader.Load(Loader.Scene.MenuScene);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
            _menuBtn.onClick.RemoveAllListeners();
        }

        private void LoadLanguague()
        {
            _pauseHeadingText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "PAUSE");
            _backBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "BACK");
            _menuBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "MENU");
        }
    }
}
