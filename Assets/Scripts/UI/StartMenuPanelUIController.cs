using Assets.Scripts.UI.Interface;

namespace Assets.Scripts.UI
{
    public class StartMenuPanelUIController : IUIController
    {
        private StartMenuPanelUIView startMenuPanelUIView;

        public StartMenuPanelUIController(StartMenuPanelUIView startMenuPanelUIView)
        {
            this.startMenuPanelUIView = startMenuPanelUIView;
            this.startMenuPanelUIView.SetController(this);
        }

        public void ToggleVisibility(bool value) => startMenuPanelUIView.ToggleUIView(value);


    }
}
