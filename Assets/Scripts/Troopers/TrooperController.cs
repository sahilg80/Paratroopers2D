using Assets.Scripts.Main;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers
{
    public class TrooperController
    {
        public TrooperView TrooperView { get; private set; }
        private TrooperScriptableObject trooperSO;
        private TrooperStateMachine trooperStateMachine;

        public TrooperController(TrooperView trooperView, TrooperScriptableObject trooperScriptableObject)
        {
            this.TrooperView = Object.Instantiate(trooperView);
            trooperSO = trooperScriptableObject;
            this.TrooperView.SetController(this);
            this.TrooperView.SubscribeEvents();
            trooperStateMachine = new TrooperStateMachine(this.TrooperView, trooperScriptableObject);
            trooperStateMachine.CreateStates();
            trooperStateMachine.InitializeState();
        }

        public void ChangeVisibilityState(bool value) => TrooperView.gameObject.SetActive(value);

        public void UpdateLoop() => trooperStateMachine.UpdateLoop();
        
        public void SetPosition(Vector3 position) => TrooperView.transform.position = position;

        public StateMachine.Troopers.TrooperState GetActiveState() => trooperStateMachine.ActiveStateValue;
        
        public void OnAttackedByBullet()
        {
            GameService.Instance.EventService.OnParaTrooperKilled.InvokeEvent(trooperSO.KillReward);
            trooperStateMachine.SwitchState(StateMachine.Troopers.TrooperState.DEAD, DeactivateTrooper);
        }

        public void MoveTrooperToTargetPosition(StateMachine.Troopers.TrooperState newState, Vector3 position)
        {
            trooperStateMachine.SwitchState(newState, null);
            trooperStateMachine.SetTargetPositionForActiveState(position);
        }

        public void ClimbOnStairs(StateMachine.Troopers.TrooperState newState, Vector3 targetPosition, List<Vector3> climbingStairs)
        {
            trooperStateMachine.SwitchState(newState, null);
            trooperStateMachine.SetTargetPositionForActiveState(targetPosition);
            trooperStateMachine.SetClimbingStairsList(climbingStairs);
        }

        public void LandedOnGround() => trooperStateMachine.SwitchState(StateMachine.Troopers.TrooperState.ONGROUND);

        private void DeactivateTrooper() => GameService.Instance.HelicopterService.ReturnTrooperToPool(this);

    }
}
