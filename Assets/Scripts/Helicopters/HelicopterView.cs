using Assets.Scripts.Bullet;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    public class HelicopterView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        private float speed;
        private HelicopterController controller;
        private event Action OnCollisionWithWall;
        private event Action OnCollisionWithBullet;
        private Vector3 directionToMove;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        void Update()
        {
            // Move the sprite
            transform.Translate(directionToMove * speed * Time.deltaTime);
        }

        public void SetDirectionToMove(Transform obj )
        {
            directionToMove = obj.right;
        }

        public void SubscribeEvents()
        {
            if (controller != null)
            {
                OnCollisionWithWall += controller.OnCollisionWithWall;
                OnCollisionWithBullet += controller.OnCollisionWithBullet;
            }
        }

        public void SetSpeed(float value) => speed = value;

        public void SetController(HelicopterController controller) => this.controller = controller;
        
        public void FlipSprite(bool value) => spriteRenderer.flipX = value;

        public void CollidedWithWall()
        {
            Debug.Log("completed my work");
            OnCollisionWithWall?.Invoke();
        }

        private void UnSubscribeEvents()
        {
            if (controller != null)
            {
                OnCollisionWithWall -= controller.OnCollisionWithWall;
                OnCollisionWithBullet -= controller.OnCollisionWithBullet;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BulletView bullet = collision.transform.GetComponent<BulletView>();
            if (bullet != null)
            {
                CollidedWithBullet();
            }
        }

        private void CollidedWithBullet()
        {
            Debug.Log("bullet hit");
            OnCollisionWithBullet?.Invoke();
        }
    }
}
