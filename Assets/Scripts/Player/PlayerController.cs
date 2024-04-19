using Assets.Scripts.Bullet;
using Assets.Scripts.Main;
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
        private bool isPlayerAlive;
        
        public PlayerController(BulletPool bulletPool, PlayerView playerView, 
            PlayerScriptableObject playerScriptableObject)
        {
            isPlayerAlive = true;
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

        public void OnPlayerKilled() => playerView.SetDeathAnimation();

        public void PlayerInput()
        {
            if (!isPlayerAlive) return;
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

        public void ShowGameOverUI()
        {
            isPlayerAlive = false;
            GameService.Instance.UIService.PlayerDeathUI();
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

            Vector3 currentRotation = bulletLauncher.rotation.eulerAngles;
            currentRotation.z = currentRotation.z > 180 ? currentRotation.z - 360 : currentRotation.z;
            currentRotation.z = Mathf.Clamp(currentRotation.z, -playerSO.MaxRotationAngle, playerSO.MaxRotationAngle);

            // Apply rotation
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
