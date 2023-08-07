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
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            GameManager.OnScoreUp += UpdateScoreUI;
        }

        private void OnDisable()
        {
            GameManager.OnScoreUp -= UpdateScoreUI;
        }


        private void Start()
        {
            UpdateScoreUI();


            _pauseBtn.onClick.AddListener(() =>
            {
                UIGameplayManager.Instance.CloseAll();
                UIGameplayManager.Instance.DisplayPauseMenu(true);
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PAUSE);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

        }

        private void OnDestroy()
        {
            _pauseBtn.onClick.RemoveAllListeners();
        }

        private void UpdateScoreUI()
        {
            _scoreText.text = GameManager.Instance.Score.ToString();
        }
    }
}
