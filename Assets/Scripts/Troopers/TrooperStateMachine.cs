using Assets.Scripts.StateMachine.Troopers;
using Assets.Scripts.Troopers.ConcreteStates;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers
{
    public class TrooperStateMachine
    {
        private TrooperBaseState activeState;
        public TrooperState ActiveStateValue { get; private set; }

        private FreeFallState freeFallState;
        private ParachuteState parachuteState;
        private DeadState deadState;
        private GroundedState groundedState;
        private WalkingState walkingState;
        private ClimbState climbState;

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
            walkingState = new WalkingState();
            climbState = new ClimbState();
        }

        public void InitializeState()
        {
            activeState = freeFallState;
            activeState.EnterState(this, null);
        }

        public void SetTargetPositionForActiveState(Vector3 position)
        {
            activeState.SetTargetPosition(position);
        }

        public void SetClimbingStairsList(List<Vector3> positionList)
        {
            activeState.SetClimbingStairsList(positionList);
        }

        public void UpdateLoop()
        {
            activeState.UpdateState(this);
        }

        public void SwitchState(TrooperState newState, Action onSuccess=null)
        {
            if (ActiveStateValue == newState) return;
            activeState.ExitState(this);
            ActiveStateValue = newState;
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
                case TrooperState.WALKING:
                    // Handle walking state
                    activeState = walkingState;
                    break;
                case TrooperState.CLIMB:
                    // Handle CLIMB state
                    activeState = climbState;
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
