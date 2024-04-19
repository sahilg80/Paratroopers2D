using Assets.Scripts.UI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI
{
    public class GameOverPanelController : IUIController
    {
        private GameOverPanelView gameOverPanelUIView;

        public GameOverPanelController(GameOverPanelView gameOverPanelUIView)
        {
            this.gameOverPanelUIView = gameOverPanelUIView;
            this.gameOverPanelUIView.SetController(this);
        }

        public void ToggleVisibility(bool value) => gameOverPanelUIView.ToggleUIView(value);

    }
}
