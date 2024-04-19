
using System;
using UnityEngine;

namespace Assets.Scripts.Troopers.AttackableTroopers
{
    public class AttackableTrooperDetector : MonoBehaviour
    {
        private event Action<TrooperView> OnGroundLanding;
        private AttackableTrooperController attackableTrooperController;

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
            if (attackableTrooperController != null)
            {
                OnGroundLanding += attackableTrooperController.TrooperLandedOnGround;
            }
        }

        private void UnSubscribeEvents()
        {
            if (attackableTrooperController != null)
            {
                OnGroundLanding -= attackableTrooperController.TrooperLandedOnGround;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TrooperView groundedObject = collision.gameObject.GetComponent<TrooperView>();
            if (groundedObject != null)
            {
                groundedObject.OnTouchGround(); 
                OnGroundLanding?.Invoke(groundedObject);
            }
        }

        public void SetController(AttackableTrooperController controller) => attackableTrooperController = controller;

    }
}
