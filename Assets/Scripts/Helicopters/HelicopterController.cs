using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    public class HelicopterController
    {
        private HelicopterView helicopterView;
        private HelicopterScriptableObject helicopterSO;
        private Transform leftSpawnLocation;
        private Transform rightSpawnLocation;

        public HelicopterController(HelicopterView helicopterPrefab, HelicopterScriptableObject helicopterScriptableObject,
            Transform leftSpawnLocation, Transform rightSpawnLocation)
        {
            helicopterSO = helicopterScriptableObject;
            this.leftSpawnLocation = leftSpawnLocation;
            this.rightSpawnLocation = rightSpawnLocation;
            helicopterView = UnityEngine.Object.Instantiate(helicopterPrefab);
            helicopterView.SetSpeed(helicopterSO.Speed);
            helicopterView.SetController(this);
            helicopterView.SubscribeEvents();
        }

        public void SetPosition(bool isRight)
        {
            if (isRight)
            {
                helicopterView.SetDirectionToMove(rightSpawnLocation);
                helicopterView.transform.position = rightSpawnLocation.position;
                FlipSprite(false);
            }
            else
            {
                helicopterView.SetDirectionToMove(leftSpawnLocation);
                helicopterView.transform.position = leftSpawnLocation.position;
                FlipSprite(true);
            }
        }

        public void ChangeVisibilityState(bool value) => helicopterView.gameObject.SetActive(value);

        private void FlipSprite(bool value) => helicopterView.FlipSprite(value);
        
        //public void OnCollisionWithWall()
        //{
        //    GameService.Instance.HelicopterService.ReturnHelicopterToPool(this);
        //}

        public void OnCollisionWithObject()
        {
            GameService.Instance.HelicopterService.ReturnHelicopterToPool(this);
        }

        public void OnAttackedByBullet()
        {
            helicopterView.ChangeColliderState(false);
            helicopterView.SetAnimationToDestroy();
        }

    }
}
