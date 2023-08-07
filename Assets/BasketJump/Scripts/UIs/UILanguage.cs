using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BasketJump
{
    public class UILanguage : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;
        [SerializeField] private Button _leftBtn;
        [SerializeField] private Button _rightBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _languageHeadingText;
        [SerializeField] private TextMeshProUGUI _backBtnText;
        [SerializeField] private TextMeshProUGUI _languageOptionText;



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
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
            });

            _leftBtn.onClick.AddListener(() =>
            {
                ToggleLanguage();
            });

            _rightBtn.onClick.AddListener(() =>
            {
                ToggleLanguage();
            });
        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
            _leftBtn.onClick.RemoveAllListeners();
            _rightBtn.onClick.RemoveAllListeners();
        }

        private void LoadLanguague()
        {         
            _languageHeadingText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "LANGUAGE");
            _backBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "BACK");      
        }

        private void UpdateLanguageText()
        {
            if (LanguageManager.Instance.CurrentLanguague == LanguageManager.Languague.English)
            {
                _languageOptionText.text = "ENGLISH";
            }
            else
            {
                _languageOptionText.text = "FRANÇAIS";
            }
        }


        private void ToggleLanguage()
        {
            LanguageManager.Instance.ChangeLanguague(
                 LanguageManager.Instance.CurrentLanguague == LanguageManager.Languague.English
                ? LanguageManager.Languague.France
                : LanguageManager.Languague.English);

            UpdateLanguageText();
        }
    }
}
