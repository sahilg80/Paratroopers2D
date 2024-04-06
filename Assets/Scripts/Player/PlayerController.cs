using Assets.Scripts.Bullet;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController
    {
        private BulletPool bulletPool;

        public PlayerController(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
        }

        private void SpawnBullet(Vector3 position)
        {
            BulletController bulletController = bulletPool.GetBullet();
            bulletController.SetPosition(position);
            bulletController.ChangeVisibilityState(true);
        }
    }
}
