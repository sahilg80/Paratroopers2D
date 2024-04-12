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
        private Transform troopersParent;
        private AttackableTrooperDetector attackableTrooperDetector;
        private Vector3 playerPosition;
        //private AttackState attackState;
        private int counter; 

        public AttackableTrooperController(AttackableTrooperServiceData attackableTrooperServiceData)
        {
            this.troopersParent = attackableTrooperServiceData.GroundedTrooperParent;
            stairsStepsRequired = attackableTrooperServiceData.StepsToClimbByTroopers;
            this.attackableTrooperDetector = attackableTrooperServiceData.attackableTrooperDetector;
            playerPosition = attackableTrooperServiceData.PlayerPosition.position;
            priorityQueue = new PriorityQueue<TrooperController>(new TrooperDistanceComparer(playerPosition));
            InitializeParameters();
        }

        public void TrooperLandedOnGround(TrooperView groundedTrooper)
        {
            counter = counter + 1;
            Debug.Log("counter " + counter);
            if (groundedTrooper.transform.position.x < playerPosition.x)
            {
                leftSideTroopersList.Add(groundedTrooper);
                Debug.Log("increase left list to " + leftSideTroopersList.Count);
            }
            else if (groundedTrooper.transform.position.x > playerPosition.x)
            {
                rightSideTroopersList.Add(groundedTrooper);
                Debug.Log("increase right list to " +rightSideTroopersList.Count);
            }
            else
            {
                Debug.Log("not added this " + groundedTrooper.transform.name);
            }
            groundedTrooper.transform.parent = troopersParent;
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
                //attackState = AttackState.FinalizingTroop;
                waitForCollectingTroopers = false;
                GameService.Instance.EventService.OnRequiredTroopersCollected.InvokeEvent();
            }
        }

        private List<TrooperView> CheckMaximumTrooperSide()
        {
            Debug.Log("left list length " + leftSideTroopersList.Count);
            Debug.Log("right list length " + rightSideTroopersList.Count);
            if (rightSideTroopersList.Count > leftSideTroopersList.Count)
                return rightSideTroopersList;
            else
                return leftSideTroopersList;
        }

        public void CreateTrooperAttackQueue()
        {
            Debug.Log("preparing troop");
            //attackState = AttackState.AttackingPlayer;

            List<TrooperView> attackableTroopers = CheckMaximumTrooperSide();
            for(int i = 0; i<attackableTroopers.Count; i++)
            {
                priorityQueue.Enqueue(attackableTroopers[i].GetController());
                Debug.Log("enqueing "+attackableTroopers[i].GetController().TrooperView.gameObject.name);
            }
            Debug.Log("queue size " + priorityQueue.Count);
            StartAttackToPlayer();
        }

        private async void StartAttackToPlayer()
        {
            await Task.Delay(1500);
            int value = priorityQueue.Count;
            while (priorityQueue.Count > value - troopersRequiredToAttack)
            {
                TrooperController closestObject = priorityQueue.Dequeue();
                Debug.Log("trooper selected to attack " + closestObject.TrooperView.gameObject.name);

                UpdateClimbingStairs(closestObject);

                while (closestObject.GetActiveState() != StateMachine.Troopers.TrooperState.COMPLETED)
                {
                    await Task.Yield();
                }

                if (targetReachedCount < currentStairStepLevel)
                {
                    targetPosition = new Vector3(targetPosition.x + 0.5f, targetPosition.y, targetPosition.z);
                    targetReachedCount += 1;
                }
                else if (currentStairStepLevel == 0)
                {
                    // kill player;
                    Debug.Log("killed player");
                }
                else
                {
                    climbingStairs.Add(new Vector3(targetPosition.x + 0.5f, targetPosition.y, targetPosition.z));
                    targetPosition = new Vector3(0, targetPosition.y, targetPosition.z);
                    targetPosition = new Vector3(targetPosition.x - (targetReachedCount-1)/2, targetPosition.y + 0.5f, targetPosition.z);
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
