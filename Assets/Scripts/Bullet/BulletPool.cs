using Assets.Scripts.Utilities;

namespace Assets.Scripts.Bullet
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        private BulletView bulletPrefab;

        public BulletPool(BulletView bulletView)
        {
            bulletPrefab = bulletView;
        }

        public BulletController GetBullet() => GetItem();

        public void ReturnBulletToPool(BulletController controller) => ReturnItem(controller);

        protected override BulletController CreateItem() => new BulletController(bulletPrefab);
        
    }
}
