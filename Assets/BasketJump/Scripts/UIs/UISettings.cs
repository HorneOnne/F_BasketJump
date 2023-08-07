using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BasketJump
{
    public class UISettings : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;

        [Header("Sliders")]
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _settingsHeadingText;
        [SerializeField] private TextMeshProUGUI _backBtnText;
        [SerializeField] private TextMeshProUGUI _soundText;
        [SerializeField] private TextMeshProUGUI _musicText;
   



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
            _soundSlider.value = SoundManager.Instance.SFXVolume;
            _musicSlider.value = SoundManager.Instance.BackgroundVolume;

            _backBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
            _musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);           
        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
            _soundSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.RemoveAllListeners();
        }

        private void LoadLanguague()
        {
            _settingsHeadingText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SETTINGS");
            _backBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "BACK");
            _soundText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SOUND");
            _musicText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "MUSIC");
        }

        private void OnSoundSliderChanged(float value)
        {
            SoundManager.Instance.SFXVolume = value;
        }

        private void OnMusicSliderChanged(float value)
        {
            SoundManager.Instance.BackgroundVolume = value;
            SoundManager.Instance.UpdateBackgroundVolume();
        }
    }
}
