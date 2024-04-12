using System;
using UnityEngine;

namespace Assets.Scripts.Troopers.ConcreteStates
{
    public class CompletedState : TrooperBaseState
    {
        public override void EnterState(TrooperStateMachine stateMachine, Action onSuccess)
        {
            Debug.Log("Entered in completed state");
        }

        public override void ExitState(TrooperStateMachine stateMachine)
        {

        }

        public override void UpdateState(TrooperStateMachine stateMachine)
        {

        }
    }
}
