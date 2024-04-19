using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Troopers.ConcreteStates
{
    public class ClimbState : TrooperBaseState
    {
        private float speed;
        private Vector3 currentPosition;
        private bool isClimbed;
        public override void EnterState(TrooperStateMachine stateMachine, Action onSuccess)
        {
            speed = stateMachine.TrooperSO.WalkingSpeed;
            Debug.Log("started climbing ");
        }

        public override void ExitState(TrooperStateMachine stateMachine)
        {

        }

        public override void UpdateState(TrooperStateMachine stateMachine)
        {
            if (ClimbingStairStepsList.Count > 0 && !isClimbed)
            {
                currentPosition = stateMachine.TrooperView.transform.localPosition;
                if (Vector3.Distance(currentPosition, ClimbingStairStepsList[0]) > 0.05f)
                {
                    stateMachine.TrooperView.transform.localPosition = Vector3.MoveTowards(currentPosition, ClimbingStairStepsList[0], speed * Time.deltaTime);
                }
                else
                {
                    isClimbed = true;
                    StartClimbingStairs(stateMachine);
                }
            }
        }

        private async void StartClimbingStairs(TrooperStateMachine stateMachine)
        {
            stateMachine.TrooperView.transform.localPosition = ClimbingStairStepsList[0];
            // delay;
            await Task.Delay(1000);

            for (int i = 1; i < ClimbingStairStepsList.Count; i++)
            {
                stateMachine.TrooperView.transform.localPosition = ClimbingStairStepsList[i];
                await Task.Delay(1500);
            }
            stateMachine.TrooperView.transform.localPosition = TargetPosition;
            stateMachine.SwitchState(StateMachine.Troopers.TrooperState.COMPLETED, null);
        }
    }
}
