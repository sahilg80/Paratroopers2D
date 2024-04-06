using Assets.Scripts.Bullet;
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
        private const float maxRotationAngle = 45f;

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
        }

        public void PlayerInput()
        {
            HandleRotate();
            HandleShoot();
        }

        private void HandleRotate()
        {
            float direction = Input.GetAxis("Horizontal");
            Quaternion deltaRotation = Quaternion.Euler(0, 0, -direction * playerSO.RotationSpeed * Time.deltaTime);

            // Apply rotation
            bulletLauncher.rotation *= deltaRotation;

            //Quaternion currentRotation = bulletLauncher.rotation *  deltaRotation;
            ////bulletLauncher.Rotate(Vector2.right, direction * playerSO.RotationSpeed * Time.deltaTime);
            //float currentZRotation = currentRotation.eulerAngles.z;

            //// Calculate the target Z rotation
            //float targetZRotation = Mathf.Clamp(currentZRotation, -maxRotationAngle, maxRotationAngle);

            //// Set the new rotation
            //bulletLauncher.rotation = Quaternion.Euler(0, 0, targetZRotation);
        }


        void HandleRot()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float rotationAmount = horizontalInput * playerSO.RotationSpeed * Time.deltaTime;

            // Calculate the current Z rotation
            float currentZRotation = bulletLauncher.rotation.eulerAngles.z;

            // Calculate the target Z rotation
            float targetZRotation = Mathf.Clamp(currentZRotation + rotationAmount, -maxRotationAngle, maxRotationAngle);

            // Set the new rotation
            bulletLauncher.rotation = Quaternion.Euler(0, 0, targetZRotation);
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
