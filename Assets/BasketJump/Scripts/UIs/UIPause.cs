using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BasketJump
{
    public class UIPause : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _continueBtn;
        [SerializeField] private Button _menuBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _continueBtnText;
        [SerializeField] private TextMeshProUGUI _menuBtnText;

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

            _continueBtn.onClick.AddListener(() =>
            {
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PLAYING);
                UIGameplayManager.Instance.CloseAll();
            });

            _menuBtn.onClick.AddListener(() =>
            {
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.EXIT);
                Loader.Load(Loader.Scene.MenuScene);
            });
        }

        private void OnDestroy()
        {
            _continueBtn.onClick.RemoveAllListeners();
            _menuBtn.onClick.RemoveAllListeners();
        }

        private void LoadLanguague()
        {
            _continueBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "CONTINUE");
            _menuBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "MENU");
        }
    }
}
