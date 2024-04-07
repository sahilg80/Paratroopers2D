using Assets.Scripts.Bullet;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    public class HelicopterView : MonoBehaviour, ICollisionHandler, IDamageable, ITriggerTrooper
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private BoxCollider2D helicopterCollider;
        [SerializeField]
        private Transform troopSpawnTransform;
        private float speed;
        private HelicopterController controller;
        private event Action OnJobDone;
        private Vector3 directionToMove;
        private bool isAlive;
        private event Action OnTriggerSpawnTrooper;
        private event Action OnHitByBullet;
        private WaitForSeconds troopSpawnRate;
        private Coroutine trooperCoroutine;

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

        private void Start()
        {
            troopSpawnRate = new WaitForSeconds(2f);
        }

        void Update()
        {
            // Move the sprite
            if(isAlive)
                transform.Translate(directionToMove * speed * Time.deltaTime);
        }

        public void SubscribeEvents()
        {
            if (controller != null)
            {
                OnJobDone += controller.DeactivateHelicopter;
                OnTriggerSpawnTrooper += controller.SpawnTrooper;
                OnHitByBullet += controller.OnAttackedByBullet;
            }
        }

        public void SetDirectionToMove(Transform target) => directionToMove = target.right;

        public Vector3 GetSpawnPosition() => troopSpawnTransform.position;

        public void SetSpeed(float value) => speed = value;

        public void SetController(HelicopterController controller) => this.controller = controller;
        
        public void FlipSprite(bool value) => spriteRenderer.flipX = value;

        // invoking this event from destroy animation key event
        public void OnDestroyAnimationComplete() => OnJobDone?.Invoke();

        // called when helicopter collides with collider placed at boundaries
        public void OnCollisionWithBoundary()
        {
            Debug.Log("completed my work");
            OnJobDone?.Invoke();
        }

        public void TakeDamage()
        {
            Debug.Log("recieved damage from bullet");
            isAlive = false;
            OnHitByBullet?.Invoke();
        }

        public void DisableHelicopter()
        {
            StopSpawningTrooper();
            ChangeColliderState(false);
            animator.SetTrigger("Destroy");
        }

        public void OnTriggerStartTroppers() => StartSpawningTrooper();

        public void OnTriggerFinishTroopers() => StopSpawningTrooper();
        
        private void StopSpawningTrooper()
        {
            if (trooperCoroutine != null)
            {
                StopCoroutine(trooperCoroutine);
            }
        }

        private IEnumerator SpawnTrooperLoop()
        {
            while (true)
            {
                OnTriggerSpawnTrooper?.Invoke();
                yield return troopSpawnRate;
            }
        }

        private void UnSubscribeEvents()
        {
            if (controller != null)
            {
                OnJobDone -= controller.DeactivateHelicopter;
                OnTriggerSpawnTrooper -= controller.SpawnTrooper;
                OnHitByBullet -= controller.OnAttackedByBullet;
            }
        }

        private void ChangeColliderState(bool value) => helicopterCollider.enabled = value;

        private void StartSpawningTrooper()
        {
            StopSpawningTrooper();
            trooperCoroutine = StartCoroutine(SpawnTrooperLoop());
        }
    }
}
