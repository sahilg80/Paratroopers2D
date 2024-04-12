using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers
{
    public abstract class TrooperBaseState
    {
        public Vector3 TargetPosition { get; private set; }
        public List<Vector3> ClimbingStairStepsList;
        public abstract void EnterState(TrooperStateMachine stateMachine, Action onSuccess);
        public abstract void UpdateState(TrooperStateMachine stateMachine);
        public abstract void ExitState(TrooperStateMachine stateMachine);

        public void SetTargetPosition(Vector3 position)
        {
            TargetPosition = position;
        }

        public void SetClimbingStairsList(List<Vector3> positionList)
        {
            ClimbingStairStepsList = positionList;
        }
    }
}
