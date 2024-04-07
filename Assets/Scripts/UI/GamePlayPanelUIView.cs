using UnityEngine;
using TMPro;
using Assets.Scripts.UI.Interface;

namespace Assets.Scripts.UI
{
    public class GamePlayPanelUIView : MonoBehaviour, IUIView
    {
        [SerializeField]
        private TextMeshProUGUI scoreText;
        private IUIController gamePlayPanelUIController;

        public void SetController(IUIController controller) => gamePlayPanelUIController = controller;

        public void ToggleUIView(bool value) => gameObject.SetActive(value);

        public void UpdateScoreText(int score) => scoreText.SetText(score.ToString());

    }
}
