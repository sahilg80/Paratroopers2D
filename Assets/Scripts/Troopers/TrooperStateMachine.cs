using Assets.Scripts.StateMachine.Troopers;
using Assets.Scripts.Troopers.ConcreteStates;
using System;

namespace Assets.Scripts.Troopers
{
    public class TrooperStateMachine
    {
        private TrooperBaseState activeState;
        private FreeFallState freeFallState;
        private ParachuteState parachuteState;
        private DeadState deadState;
        private GroundedState groundedState;

        public TrooperView TrooperView { get; private set; }
        public TrooperScriptableObject TrooperSO { get; private set; }

        public TrooperStateMachine(TrooperView trooperView, TrooperScriptableObject trooperScriptableObject)
        {
            this.TrooperView = trooperView;
            TrooperSO = trooperScriptableObject;
        }

        public void CreateStates()
        {
            freeFallState = new FreeFallState();
            parachuteState = new ParachuteState();
            deadState = new DeadState();
            groundedState = new GroundedState();
        }

        public void InitializeState()
        {
            activeState = freeFallState;
            activeState.EnterState(this, null);
        }

        public void UpdateLoop()
        {
            activeState.UpdateState(this);
        }

        public void SwitchState(TrooperState newState, Action onSuccess)
        {
            switch (newState)
            {
                case TrooperState.FREEFALL:
                    // Handle FREEFALL state
                    activeState = freeFallState;
                    break;
                case TrooperState.PARACHUTE:
                    // Handle PARACHUTE state
                    activeState = parachuteState;
                    break;
                case TrooperState.ONGROUND:
                    // Handle ONPLATFORM state
                    activeState = groundedState;
                    break;
                case TrooperState.CLIMB:
                    // Handle CLIMB state
                    break;
                case TrooperState.COMPLETED:
                    // Handle COMPLETED state
                    break;
                case TrooperState.DEAD:
                    // Handle DEAD state
                    activeState = deadState;
                    break;
                default:
                    // Handle default case (if needed)
                    break;
            }
            activeState.EnterState(this, onSuccess);
        }
    }
}
