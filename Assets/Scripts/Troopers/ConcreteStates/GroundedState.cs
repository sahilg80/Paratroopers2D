using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers.ConcreteStates
{
    public class GroundedState : TrooperBaseState
    {
        public override void EnterState(TrooperStateMachine stateMachine, Action onSuccess)
        {
            stateMachine.TrooperView.SetTrooperSprite(stateMachine.TrooperSO.ParatrooperSprite);
        }

        public override void ExitState(TrooperStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        public override void UpdateState(TrooperStateMachine stateMachine)
        {

        }
    }
}
