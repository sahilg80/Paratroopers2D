using Assets.Scripts.Bullet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerService
    {
        private BulletPool bulletPool;
        private PlayerController playerController;

        public PlayerService(BulletView bulletPrefab, BulletScriptableObject bulletScriptableObject,
            PlayerView playerView, PlayerScriptableObject playerScriptableObject)
        {
            bulletPool = new BulletPool(bulletPrefab, bulletScriptableObject);
            playerController = new PlayerController(bulletPool, playerView, playerScriptableObject);
        }

        public void ReturnBulletToPool(BulletController bulletToReturn)
        {
            bulletPool.ReturnBulletToPool(bulletToReturn);
            bulletToReturn.ChangeVisibilityState(false);
        }

    }
}
