using Assets.Scripts.Helicopters;
using Assets.Scripts.Main;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Troopers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Troopers.AttackableTroopers
{
    public class AttackableTrooperController
    {

        private List<TrooperView> leftSideTroopersList;
        private List<TrooperView> rightSideTroopersList;
        private int stairsStepsRequired;
        private int troopersRequiredToAttack;
        private PriorityQueue<TrooperController> priorityQueue;
        private bool waitForCollectingTroopers;
        private Vector3 targetPosition;
        private List<Vector3> climbingStairs;
        private int currentStairStepLevel;
        private int targetReachedCount;
        private Transform rightSideTroopersParent;
        private Transform leftSideTroopersParent;
        private AttackableTrooperDetector attackableTrooperDetector;
        private Vector3 playerPosition;
        private int direction;

        public AttackableTrooperController(AttackableTrooperServiceData attackableTrooperServiceData)
        {
            this.rightSideTroopersParent = attackableTrooperServiceData.RightSideGroundedTrooperParent;
            this.leftSideTroopersParent = attackableTrooperServiceData.LeftSideGroundedTrooperParent;
            stairsStepsRequired = attackableTrooperServiceData.StepsToClimbByTroopers;
            this.attackableTrooperDetector = attackableTrooperServiceData.attackableTrooperDetector;
            playerPosition = attackableTrooperServiceData.PlayerPosition.position;
            priorityQueue = new PriorityQueue<TrooperController>(new TrooperDistanceComparer(playerPosition));
            InitializeParameters();
        }

        public void TrooperLandedOnGround(TrooperView groundedTrooper)
        {
            if (groundedTrooper.transform.position.x < playerPosition.x)
            {
                leftSideTroopersList.Add(groundedTrooper);
                groundedTrooper.transform.parent = leftSideTroopersParent;
            }
            else if (groundedTrooper.transform.position.x > playerPosition.x)
            {
                rightSideTroopersList.Add(groundedTrooper);
                groundedTrooper.transform.parent = rightSideTroopersParent;
            }
        }

        public void UpdateLoop()
        {
            if(waitForCollectingTroopers)
                CheckTrooperRequiredCompleted();
        }

        private void InitializeParameters()
        {
            targetPosition = Vector3.zero;
            climbingStairs = new List<Vector3>();
            targetReachedCount = 1;
            waitForCollectingTroopers = true;
            currentStairStepLevel = stairsStepsRequired;
            leftSideTroopersList = new List<TrooperView>();
            rightSideTroopersList = new List<TrooperView>();
            attackableTrooperDetector.SetController(this);
            attackableTrooperDetector.SubscribeEvents();
            SetTroopersRequiredToAttack();
        }

        private void SetTroopersRequiredToAttack()
        {
            int sum = 0;
            for (int i = 1; i <= stairsStepsRequired; i++)
            {
                sum += i;
            }
            troopersRequiredToAttack = sum+1;
        }

        private void CheckTrooperRequiredCompleted()
        {
            if(rightSideTroopersList.Count >= troopersRequiredToAttack || leftSideTroopersList.Count >= troopersRequiredToAttack)
            {
                waitForCollectingTroopers = false;
                GameService.Instance.EventService.OnRequiredTroopersCollected.InvokeEvent();
            }
        }

        public void CreateTrooperAttackQueue()
        {
            List<TrooperView> attackableTroopers = CheckMaximumTrooperSide();
            for(int i = 0; i<attackableTroopers.Count; i++)
            {
                priorityQueue.Enqueue(attackableTroopers[i].GetController());
            }

            StartAttackToPlayer();
        }

        private List<TrooperView> CheckMaximumTrooperSide()
        {
            if (rightSideTroopersList.Count > leftSideTroopersList.Count)
            {
                direction = 1;
                return rightSideTroopersList;
            }
            else
            {
                direction = -1;
                return leftSideTroopersList;
            }
        }

        private async void StartAttackToPlayer()
        {
            await Task.Delay(1500);
            int value = priorityQueue.Count;

            while (priorityQueue.Count > value - troopersRequiredToAttack)
            {
                TrooperController closestObject = priorityQueue.Dequeue();

                UpdateClimbingStairs(closestObject);

                while (closestObject.GetActiveState() != StateMachine.Troopers.TrooperState.COMPLETED)
                {
                    await Task.Yield();
                }

                if (targetReachedCount < currentStairStepLevel)
                {
                    targetPosition = new Vector3(targetPosition.x + (direction * 0.5f), targetPosition.y, targetPosition.z);
                    targetReachedCount += 1;
                }
                else if (currentStairStepLevel == 0)
                {
                    // kill player;
                    await Task.Delay(2000);
                    Debug.Log("killed player");
                    GameService.Instance.EventService.OnPlayerDeath.InvokeEvent();
                }
                else
                {
                    climbingStairs.Add(new Vector3(targetPosition.x + (direction * 0.5f), targetPosition.y, targetPosition.z));
                    targetPosition = new Vector3(0, targetPosition.y + 0.5f, targetPosition.z);

                    targetReachedCount = 1;
                    currentStairStepLevel -= 1;
                }

                await Task.Delay(1500);
            }
        }


        private void UpdateClimbingStairs(TrooperController closestObject)
        {
            if (targetPosition.y > 0)
            {
                FollowClimb(closestObject);
            }
            else
            {
                MoveTowardsTarget(closestObject);
            }
        }

        private void MoveTowardsTarget(TrooperController closestObject)
        {
            closestObject.MoveTrooperToTargetPosition(StateMachine.Troopers.TrooperState.WALKING, targetPosition);
        }

        private void FollowClimb(TrooperController closestObject)
        {
            closestObject.ClimbOnStairs(StateMachine.Troopers.TrooperState.CLIMB, targetPosition, climbingStairs);
        }
    }

}
