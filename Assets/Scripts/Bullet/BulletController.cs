using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bullet
{
    public class BulletController
    {
        private BulletScriptableObject bulletSO;
        private BulletView bulletView;

        public BulletController(BulletView bulletPrefab, BulletScriptableObject bulletScriptableObject)
        {
            bulletView = UnityEngine.Object.Instantiate(bulletPrefab);
            bulletSO = bulletScriptableObject;
            bulletView.SetController(this);
            bulletView.SetSpeed(bulletSO.Speed);
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

        public void OnCollision()
        {
            Debug.Log("bullet hit target");
            GameService.Instance.PlayerService.ReturnBulletToPool(this);
        }
    }
}
