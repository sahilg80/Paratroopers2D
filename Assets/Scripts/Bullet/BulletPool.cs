using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Bullet
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        private BulletView bulletPrefab;
        private BulletScriptableObject bulletSO;

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject)
        {
            bulletPrefab = bulletView;
            bulletSO = bulletScriptableObject;
        }

        public BulletController GetBullet() => GetItem();

        public void ReturnBulletToPool(BulletController controller) => ReturnItem(controller);

        protected override BulletController CreateItem() => new BulletController(bulletPrefab, bulletSO);
        
    }
}
