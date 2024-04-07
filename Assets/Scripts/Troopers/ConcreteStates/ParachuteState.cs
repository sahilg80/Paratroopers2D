using System;

namespace Assets.Scripts.Troopers.ConcreteStates
{
    public class ParachuteState : TrooperBaseState
    {
        public override void EnterState(TrooperStateMachine stateMachine, Action onSuccess)
        {
            stateMachine.TrooperView.SetTrooperSprite(stateMachine.TrooperSO.ParachuteSprite);
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
