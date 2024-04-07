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
        private TrooperStateMachine trooperStateMachine;

        public TrooperController(TrooperView trooperView, TrooperScriptableObject trooperScriptableObject)
        {
            this.trooperView = UnityEngine.Object.Instantiate(trooperView);
            trooperSO = trooperScriptableObject;
            this.trooperView.SetController(this);
            this.trooperView.SubscribeEvents();
            trooperStateMachine = new TrooperStateMachine(this.trooperView, trooperScriptableObject);
            trooperStateMachine.CreateStates();
            trooperStateMachine.InitializeState();
            //this.trooperView.SetStateMachine(trooperStateMachine);
        }

        public void ChangeVisibilityState(bool value) => trooperView.gameObject.SetActive(value);

        public void UpdateLoop() => trooperStateMachine.UpdateLoop();
        
        public void SetPosition(Vector3 position) => trooperView.transform.position = position;
        
        public void OnAttackedByBullet()
        {
            Debug.Log("bullet hit target trooper");
            GameService.Instance.EventService.OnParaTrooperKilled.InvokeEvent(trooperSO.KillReward);
            trooperStateMachine.SwitchState(StateMachine.Troopers.TrooperState.DEAD, DeactivateTrooper);
            //rooperView.DestroyTrooper(trooperSO.DeathSprite, DeactivateTrooper );
        }

        public void LandedOnGround() => trooperStateMachine.SwitchState(StateMachine.Troopers.TrooperState.ONGROUND, null);

        private void DeactivateTrooper() => GameService.Instance.HelicopterService.ReturnTrooperToPool(this);

    }
}
