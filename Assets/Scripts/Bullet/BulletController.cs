using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bullet
{
    public class BulletController
    {
        private BulletView bulletView;

        public BulletController(BulletView bulletPrefab)
        {
            bulletView = UnityEngine.Object.Instantiate(bulletPrefab);
            bulletView.SetController(this);
            bulletView.SubscribeEvents();
        }

        public void ChangeVisibilityState(bool value) => bulletView.gameObject.SetActive(value);

        public void SetPosition(Vector3 position)
        {
            bulletView.transform.position = position;
        }

        public void SetOrientation(Quaternion rotation)
        {
            bulletView.transform.rotation = rotation;
        }

        public void FireInDirection(float speed, Transform direction)
        {
            bulletView.GetRigidBody().velocity = speed * direction.up;
        }

        public void DeactivateBullet()
        {
            Debug.Log("bullet hit target");
            GameService.Instance.PlayerService.ReturnBulletToPool(this);
        }
    }
}
