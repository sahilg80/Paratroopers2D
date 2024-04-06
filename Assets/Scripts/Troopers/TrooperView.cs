using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers
{
    public class TrooperView : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private BoxCollider2D trooperCollider;
        [SerializeField]
        private Rigidbody2D trooperRigidBody;
        private TrooperController trooperController;
        private event Action OnHitByBullet;
        private WaitForSeconds deathDelay;

        private void OnEnable()
        {
            ChangeColliderState(true);
            ChangeRigidBodyType(true);
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            deathDelay = new WaitForSeconds(2f);
        }

        public void SubscribeEvents()
        {
            if (trooperController != null)
            {
                OnHitByBullet += trooperController.OnAttackedByBullet;
            }
        }

        public void SetController(TrooperController controller) => trooperController = controller;

        public void TakeDamage() => OnHitByBullet?.Invoke();

        public void DestroyTrooper(Sprite sprite, Action onSuccess)
        {
            ChangeColliderState(false);
            ChangeRigidBodyType(false);
            StartCoroutine(ShowDeath(sprite, onSuccess));
        }

        private IEnumerator ShowDeath(Sprite sprite, Action onSuccess)
        {
            spriteRenderer.sprite = sprite;
            yield return deathDelay;
            onSuccess?.Invoke();
        }

        private void ChangeColliderState(bool value) => trooperCollider.enabled = value;

        private void ChangeRigidBodyType(bool value) => trooperRigidBody.simulated = value;

        private void UnSubscribeEvents()
        {
            if (trooperController != null)
            {
                OnHitByBullet -= trooperController.OnAttackedByBullet;
            }
        }

    }
}
