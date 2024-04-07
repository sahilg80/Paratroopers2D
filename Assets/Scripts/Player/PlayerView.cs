using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Transform playerShootBarrelDirection;
        [SerializeField]
        private Transform bulletLauncher;
        private PlayerController playerController;
        private event Action OnPlayerInputRecieved;

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            OnPlayerInputRecieved?.Invoke();
        }

        public Transform GetPlayerShootDirection() => playerShootBarrelDirection;
        public Transform GetBulletLauncher() => bulletLauncher;

        public void SubscribeEvents()
        {
            if (playerController != null)
            {
                OnPlayerInputRecieved += playerController.PlayerInput;
            }
        }

        private void UnSubscribeEvents()
        {
            if (playerController != null)
            {
                OnPlayerInputRecieved -= playerController.PlayerInput;
            }
        }

        public void SetController(PlayerController controller) => playerController = controller;
    }
}
