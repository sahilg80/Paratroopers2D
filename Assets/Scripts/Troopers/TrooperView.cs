using Assets.Scripts.Interfaces;
using System;
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
        private event Action OnUpdateLoop;
        private event Action OnGroundLanding;

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

        private void Update()
        {
            OnUpdateLoop?.Invoke();
        }

        public void SubscribeEvents()
        {
            if (trooperController != null)
            {
                OnHitByBullet += trooperController.OnAttackedByBullet;
                OnUpdateLoop += trooperController.UpdateLoop;
                OnGroundLanding += trooperController.LandedOnGround;
            }
        }

        public TrooperController GetController() => trooperController;

        public void SetController(TrooperController controller) => trooperController = controller;

        public void OnTouchGround() => OnGroundLanding?.Invoke();

        public void TakeDamage() => OnHitByBullet?.Invoke();

        public void SetTrooperSprite(Sprite sprite) => spriteRenderer.sprite = sprite;

        public void DisableTrooper()
        {
            ChangeColliderState(false);
            ChangeRigidBodyType(false);
        }

        private void ChangeColliderState(bool value) => trooperCollider.enabled = value;

        public void ChangeRigidBodyType(bool value) => trooperRigidBody.simulated = value;

        private void UnSubscribeEvents()
        {
            if (trooperController != null)
            {
                OnHitByBullet -= trooperController.OnAttackedByBullet;
                OnUpdateLoop -= trooperController.UpdateLoop;
                OnGroundLanding -= trooperController.LandedOnGround;
            }
        }

    }
}
