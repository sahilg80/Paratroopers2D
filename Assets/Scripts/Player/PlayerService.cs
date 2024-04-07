using Assets.Scripts.Bullet;
using Assets.Scripts.Main;

namespace Assets.Scripts.Player
{
    public class PlayerService
    {
        private BulletPool bulletPool;
        private PlayerController playerController;

        public PlayerService(BulletView bulletPrefab, PlayerView playerView, PlayerScriptableObject playerScriptableObject)
        {
            bulletPool = new BulletPool(bulletPrefab);
            playerController = new PlayerController(bulletPool, playerView, playerScriptableObject);
        }

        public void ReturnBulletToPool(BulletController bulletToReturn)
        {
            bulletPool.ReturnBulletToPool(bulletToReturn);
            bulletToReturn.ChangeVisibilityState(false);
        }

        public void SubscribeEvents()
        {
            GameService.Instance.EventService.OnParaTrooperKilled.AddListener(playerController.OnKilledTarget);
        }

        public void UnSubscribeEvents()
        {
            GameService.Instance.EventService.OnParaTrooperKilled.RemoveListener(playerController.OnKilledTarget);
        }

    }
}
