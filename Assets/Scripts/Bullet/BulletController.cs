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

        public void OnCollision()
        {
            Debug.Log("bullet hit target");
        }
    }
}
