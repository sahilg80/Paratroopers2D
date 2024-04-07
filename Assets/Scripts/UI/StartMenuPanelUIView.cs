using Assets.Scripts.UI.Interface;
using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StartMenuPanelUIView : MonoBehaviour, IUIView
    {
        private IUIController startMenuPanelUIController;

        public void SetController(IUIController controller) => startMenuPanelUIController = controller;

        public void ToggleUIView(bool value) => gameObject.SetActive(value);
        
    }
}
