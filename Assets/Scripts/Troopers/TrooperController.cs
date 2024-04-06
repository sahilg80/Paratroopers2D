using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers
{
    public class TrooperController
    {
        private TrooperView trooperView;
        private TrooperScriptableObject trooperSO;

        public TrooperController(TrooperView trooperView, TrooperScriptableObject trooperScriptableObject)
        {
            this.trooperView = UnityEngine.Object.Instantiate(trooperView);
            trooperSO = trooperScriptableObject;
            this.trooperView.SetController(this);
            this.trooperView.SubscribeEvents();
        }

        public void ChangeVisibilityState(bool value) => trooperView.gameObject.SetActive(value);

        public void SetPosition(Vector3 position)
        {
            trooperView.transform.position = position;
        }

        public void OnAttackedByBullet()
        {
            Debug.Log("bullet hit target trooper");
            trooperView.DestroyTrooper(trooperSO.SpriteAfterDestroy, DeactivateTrooper );
        }

        private void DeactivateTrooper() => GameService.Instance.HelicopterService.ReturnTrooperToPool(this);

    }
}
