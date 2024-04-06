using Assets.Scripts.StateMachine;
using System;
using UnityEngine;

namespace Assets.Scripts.Troopers.ConcreteStates
{
    public class FreeFallState : TrooperBaseState
    {
        private float timeElapsed;
        private const float waitTime = 1.4f;
        public override void EnterState(TrooperStateMachine stateMachine, Action onSuccess)
        {
            stateMachine.TrooperView.SetTrooperSprite(stateMachine.TrooperSO.CrateSprite);
            timeElapsed = 0f;
        }

        public override void ExitState(TrooperStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        public override void UpdateState(TrooperStateMachine stateMachine)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed > waitTime)
            {
                stateMachine.SwitchState(StateMachine.Troopers.TrooperState.PARACHUTE, null);
            }
        }
    }
}
