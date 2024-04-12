using System;
using UnityEngine;

namespace Assets.Scripts.Troopers.ConcreteStates
{
    public class DeadState : TrooperBaseState
    {
        private float timeElapsed;
        private Action onSuccess;
        private const float waitTime = 2f;
        public override void EnterState(TrooperStateMachine stateMachine, Action onSuccess)
        {
            stateMachine.TrooperView.DisableTrooper();
            stateMachine.TrooperView.SetTrooperSprite(stateMachine.TrooperSO.DeathSprite);
            this.onSuccess = onSuccess;
            timeElapsed = 0f;
        }

        public override void ExitState(TrooperStateMachine stateMachine)
        {

        }

        public override void UpdateState(TrooperStateMachine stateMachine)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > waitTime)
            {
                onSuccess?.Invoke();
            }
        }
    }
}
