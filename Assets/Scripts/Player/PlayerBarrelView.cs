
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBarrelView : MonoBehaviour
    {
        private PlayerController playerController;

        public void SetController(PlayerController controller) => playerController = controller;

        // set call of this function in animation window by adding key frame
        public void SetDeathUI() => playerController.ShowGameOverUI();
    }
}
