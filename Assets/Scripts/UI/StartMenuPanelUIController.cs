using Assets.Scripts.UI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
