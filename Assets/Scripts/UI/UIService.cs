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

        [Header("GameOverPanel")]
        [SerializeField]
        private GameOverPanelView gameOverPanelView;
        private GameOverPanelController gameOverPanelController;

        public void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnStartGame.AddListener(OnGameStarted);
            //GameService.Instance.EventService.OnPlayerDeath.AddListener(PlayerDeathUI);
        }
        public void UnSubscribeToEvents()
        {
            GameService.Instance.EventService.OnStartGame.RemoveListener(OnGameStarted);
            //GameService.Instance.EventService.OnPlayerDeath.RemoveListener(PlayerDeathUI);
        }

        private void Start()
        {
            gamePlayPanelUIController = new GamePlayPanelUIController(gamePlayPanelView);
            startMenuPanelUIController = new StartMenuPanelUIController(startMenuPanelUIView);
            gameOverPanelController = new GameOverPanelController(gameOverPanelView);
        }

        public void OnGameStarted()
        {
            ToggleStartMenuPanelUI(false);
            ToggleGamePlayPanelUI(true);
            ToggleGameOverPanel(false);
        }

        public void PlayerDeathUI() => ToggleGameOverPanel(true);

        private void ToggleGamePlayPanelUI(bool value) => gamePlayPanelUIController.ToggleVisibility(value);

        private void ToggleStartMenuPanelUI(bool value) => startMenuPanelUIController.ToggleVisibility(value);

        private void ToggleGameOverPanel(bool value) => gameOverPanelController.ToggleVisibility(value);

        public void OnKilledParatrooper(int updatedScore) => gamePlayPanelUIController.UpdateScore(updatedScore);
    }
}
