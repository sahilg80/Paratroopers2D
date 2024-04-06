using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private BulletController controller;
        private float speed;
        private event Action OnCollisionWIthTarget;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        public void SubscribeEvents()
        {
            if (controller != null)
            {
                OnCollisionWIthTarget += controller.OnCollision;
            }
        }

        private void UnSubscribeEvents()
        {
            if (controller != null)
            {
                OnCollisionWIthTarget -= controller.OnCollision;
            }
        }


        public void SetController(BulletController bulletController) => this.controller = bulletController;

        public void SetSpeed(float value) => speed = value;

        public void OnCollisionWithTarget()
        {

        }

    }
}
