using Assets.Scripts.Main;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("GamePlayPanel")]
        [SerializeField]
        private GamePlayPanelUIView gamePlayPanelView;
        private GamePlayPanelUIController gamePlayPanelUIController;

        [Header("StartGamePanel")]
        [SerializeField]
        private StartMenuPanelUIView startMenuPanelUIView;
        private StartMenuPanelUIController startMenuPanelUIController;

        public void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnStartGame.AddListener(OnGameStarted);
        }
        public void UnSubscribeToEvents()
        {
            GameService.Instance.EventService.OnStartGame.RemoveListener(OnGameStarted);
        }

        private void Start()
        {
            gamePlayPanelUIController = new GamePlayPanelUIController(gamePlayPanelView);
            startMenuPanelUIController = new StartMenuPanelUIController(startMenuPanelUIView);
        }

        public void OnGameStarted()
        {
            ToggleStartMenuPanelUI(false);
            ToggleGamePlayPanelUI(true);
        }

        private void ToggleGamePlayPanelUI(bool value) => gamePlayPanelUIController.ToggleVisibility(value);

        private void ToggleStartMenuPanelUI(bool value) => startMenuPanelUIController.ToggleVisibility(value);

        public void OnKilledParatrooper(int updatedScore) => gamePlayPanelUIController.UpdateScore(updatedScore);
    }
}
