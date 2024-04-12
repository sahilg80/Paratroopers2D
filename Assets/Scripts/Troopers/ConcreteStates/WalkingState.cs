using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers.ConcreteStates
{
    public class WalkingState : TrooperBaseState
    {
        private float speed;
        private Vector3 currentPosition;

        public override void EnterState(TrooperStateMachine stateMachine, Action onSuccess)
        {
            speed = stateMachine.TrooperSO.WalkingSpeed;
            Debug.Log("walk started");
        }

        public override void ExitState(TrooperStateMachine stateMachine)
        {

        }

        public override void UpdateState(TrooperStateMachine stateMachine)
        {
            currentPosition = stateMachine.TrooperView.transform.localPosition;
            if (Vector3.Distance(currentPosition, TargetPosition) > 0.05f)
            {
                stateMachine.TrooperView.transform.localPosition = Vector3.MoveTowards(currentPosition, TargetPosition, speed * Time.deltaTime);
            }
            else
            {
                stateMachine.TrooperView.transform.localPosition = TargetPosition;
                stateMachine.SwitchState(StateMachine.Troopers.TrooperState.COMPLETED, null);
            }
        }
    }
}
