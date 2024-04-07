using Assets.Scripts.UI.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GamePlayPanelUIController : IUIController
    {
        private GamePlayPanelUIView gamePlayPanelUIView;

        public GamePlayPanelUIController(GamePlayPanelUIView gamePlayPanelUIView)
        {
            this.gamePlayPanelUIView = gamePlayPanelUIView;
            this.gamePlayPanelUIView.SetController(this);
        }

        public void ToggleVisibility(bool value) => gamePlayPanelUIView.ToggleUIView(value);

        public void UpdateScore(int score) => gamePlayPanelUIView.UpdateScoreText(score);
    }
}
