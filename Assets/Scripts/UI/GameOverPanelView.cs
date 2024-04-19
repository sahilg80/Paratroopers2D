using Assets.Scripts.UI.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOverPanelView : MonoBehaviour, IUIView
    {
        private IUIController gameOverPanelUIController;
        public void SetController(IUIController controller) => gameOverPanelUIController = controller;
        public void ToggleUIView(bool value) => gameObject.SetActive(value);

    }
}
