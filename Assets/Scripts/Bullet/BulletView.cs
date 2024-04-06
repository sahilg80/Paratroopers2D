using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bullet
{
    public class BulletView : MonoBehaviour, ICollisionHandler
    {
        [SerializeField]
        private Rigidbody2D bulletRigidbody2D;
        private BulletController controller;
        private float speed;
        private event Action OnCollision;

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
                OnCollision += controller.OnCollision;
            }
        }

        private void UnSubscribeEvents()
        {
            if (controller != null)
            {
                OnCollision -= controller.OnCollision;
            }
        }

        public Rigidbody2D GetRigidBody() => bulletRigidbody2D;
        public void SetController(BulletController bulletController) => this.controller = bulletController;

        public void SetSpeed(float value) => speed = value;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            IDamageable collidedObject = collision.gameObject.GetComponent<IDamageable>();
            if (collidedObject != null)
            {
                collidedObject.TakeDamage();
                OnCollision?.Invoke();
            }
        }

        //public void OnCollisionWithTarget()
        //{

        //}

        public void OnCollisionDetected()
        {
            Debug.Log("bullet completed my work");
            OnCollision?.Invoke();
        }
    }
}
