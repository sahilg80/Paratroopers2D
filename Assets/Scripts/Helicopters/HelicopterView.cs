using Assets.Scripts.Bullet;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    public class HelicopterView : MonoBehaviour, ICollisionHandler, IDamageable
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private BoxCollider2D helicopterCollider;
        private float speed;
        private HelicopterController controller;
        private event Action OnHitByBullet;
        private event Action OnCollision;
        private Vector3 directionToMove;
        private bool isAlive;

        private void OnEnable()
        {
            isAlive = true;
            ChangeColliderState(true);
            SubscribeEvents();
        }

        private void OnDisable()
        {
            isAlive = false;
            UnSubscribeEvents();
        }

        void Update()
        {
            // Move the sprite
            if(isAlive)
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
                OnHitByBullet += controller.OnAttackedByBullet;
                OnCollision += controller.OnCollisionWithObject;
            }
        }

        public void ChangeColliderState(bool value) => helicopterCollider.enabled = value;

        public void SetSpeed(float value) => speed = value;

        public void SetController(HelicopterController controller) => this.controller = controller;
        
        public void FlipSprite(bool value) => spriteRenderer.flipX = value;

        //public void CollidedWithWall()
        //{
        //    Debug.Log("completed my work");
        //    OnCollisionWithWall?.Invoke();
        //}

        private void UnSubscribeEvents()
        {
            if (controller != null)
            {
                OnHitByBullet -= controller.OnAttackedByBullet;
                OnCollision -= controller.OnCollisionWithObject;
            }
        }

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    BulletView bullet = collision.transform.GetComponent<BulletView>();
        //    if (bullet != null)
        //    {
        //        CollidedWithBullet();
        //    }
        //}

        //private void CollidedWithBullet()
        //{
        //    Debug.Log("bullet hit");
        //    OnCollision?.Invoke();
        //}

        // invoking this event from destroy animation key event
        public void OnDestroyAnimationComplete() => OnCollision?.Invoke();

        public void SetAnimationToDestroy() => animator.SetTrigger("Destroy");

        public void OnCollisionDetected()
        {
            Debug.Log("completed my work");
            OnCollision?.Invoke();
        }

        public void TakeDamage()
        {
            Debug.Log("recieved damage from bullet");
            isAlive = false;
            OnHitByBullet?.Invoke();
        }
    }
}
