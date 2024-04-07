using Assets.Scripts.Bullet;
using Assets.Scripts.Main;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController
    {
        private BulletPool bulletPool;
        private PlayerView playerView;
        private Transform playerShootBarrelDirection;
        private Transform bulletLauncher;
        private PlayerScriptableObject playerSO;
        private PlayerModel playerModel;
        
        public PlayerController(BulletPool bulletPool, PlayerView playerView, 
            PlayerScriptableObject playerScriptableObject)
        {
            this.bulletPool = bulletPool;
            this.playerView = playerView;
            playerSO = playerScriptableObject;
            playerShootBarrelDirection = this.playerView.GetPlayerShootDirection();
            bulletLauncher = this.playerView.GetBulletLauncher();
            this.playerView.SetController(this);
            this.playerView.SubscribeEvents();
            playerModel = new PlayerModel();
        }

        public void OnKilledTarget(int scoreToAdd)
        {
            playerModel.SetScore(scoreToAdd);
            GameService.Instance.UIService.OnKilledParatrooper(playerModel.PlayerScore);
        }

        public void PlayerInput()
        {
            if (!playerModel.IsGameStarted)
            {
                HandleGameStartInput();
            }
            else
            {
                HandleRotate();
                HandleShoot();
            }
        }

        private void HandleGameStartInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameService.Instance.EventService.OnStartGame.InvokeEvent();
                playerModel.SetGamePlayStarted(true);
            }
        }

        private void HandleRotate()
        {
            float direction = Input.GetAxis("Horizontal");
            Quaternion deltaRotation = Quaternion.Euler(0, 0, -direction * playerSO.RotationSpeed * Time.deltaTime);

            // Apply rotation
            bulletLauncher.rotation *= deltaRotation;

        }

        void HandleRot()
        {
            float direction = Input.GetAxis("Horizontal");
            float rotationAmount = -direction * playerSO.RotationSpeed * Time.deltaTime;
            bulletLauncher.Rotate(0f, 0f, rotationAmount);

            // Clamp rotation to maxRotationAngle
            Vector3 currentRotation = bulletLauncher.rotation.eulerAngles;
            currentRotation.z = Mathf.Clamp(currentRotation.z, -45f, 45f);
            Debug.Log("rotating value " + currentRotation);
            bulletLauncher.rotation = Quaternion.Euler(currentRotation);
        }

        private void Handle()
        {
            float direction = Input.GetAxis("Horizontal");
            Quaternion deltaRotation = Quaternion.Euler(0, 0, -direction * playerSO.RotationSpeed * Time.deltaTime);

            // Apply rotation
            bulletLauncher.rotation *= deltaRotation;

            // Clamp rotation to maxRotationAngle
            Vector3 currentRotation = bulletLauncher.rotation.eulerAngles;
            float val = Math.Abs(currentRotation.z);
            val = Mathf.Clamp(val, 0, 45f);
            if (currentRotation.z < 0)
            {
                val = -val;
            }
            currentRotation.z = val;

            Debug.Log("rotating value " + currentRotation);
            bulletLauncher.rotation = Quaternion.Euler(currentRotation);
        }

        private void HandleShoot()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                BulletController controller = SpawnBullet();
                controller.FireInDirection(playerSO.BulletLaunchSpeed, playerShootBarrelDirection);
            }
        }

        private BulletController SpawnBullet()
        {
            BulletController bulletController = bulletPool.GetBullet();
            bulletController.SetPosition(playerShootBarrelDirection.position);
            bulletController.SetOrientation(playerShootBarrelDirection.rotation);
            bulletController.ChangeVisibilityState(true);
            return bulletController;
        }

    }
}
