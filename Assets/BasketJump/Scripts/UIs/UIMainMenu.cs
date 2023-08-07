using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BasketJump
{
    public class UIMainMenu : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _settingsBtn;
        [SerializeField] private Button _languageBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _playBtnText;
        [SerializeField] private TextMeshProUGUI _settingsBtnText;
        [SerializeField] private TextMeshProUGUI _languageBtnText;



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

            _playBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameplayScene);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _settingsBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplaySettingsMenu(true);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _languageBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayLanguageMenu(true);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _settingsBtn.onClick.RemoveAllListeners();
            _languageBtn.onClick.RemoveAllListeners();
        }

        private void LoadLanguague()
        {
            if (LanguageManager.Instance.CurrentLanguague == LanguageManager.Languague.France)
           {
                _playBtnText.fontSize = 31;
                _settingsBtnText.fontSize = 16;
                _languageBtnText.fontSize = 16;
           }
           else
           {
                _playBtnText.fontSize = 35;
                _settingsBtnText.fontSize = 20;
                _languageBtnText.fontSize = 20;
           }


            _playBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "PLAY");
            _settingsBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SETTINGS");
            _languageBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "LANGUAGE");
        }
    }
}
