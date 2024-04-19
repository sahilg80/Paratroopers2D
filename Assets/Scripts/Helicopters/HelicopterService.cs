
using Assets.Scripts.Main;
using Assets.Scripts.Troopers;
using Assets.Scripts.Troopers.AttackableTroopers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    public class HelicopterService
    {
        private HelicopterPool helicopterPool;
        private TrooperPool trooperPool;
        private bool isTroopersCollected;
        private const float spawnRate = 2.7f;

        public HelicopterService(HelicopterView helicopterPrefab, HelicopterScriptableObject helicopterSO,
            Transform leftSpawnLocation, Transform rightSpawnLocation,
            TrooperView trooperPrefab, TrooperScriptableObject trooperScriptableObject)
        {
            trooperPool = new TrooperPool(trooperPrefab, trooperScriptableObject);
            helicopterPool = new HelicopterPool(helicopterPrefab, helicopterSO,
                leftSpawnLocation, rightSpawnLocation, trooperPool);
        }

        // spawn helicopter will run on loop after every fixed interval of time
        // helicopter will be spawned in alternate way from left to right
        public IEnumerator UpdateLoop()
        {
            WaitForSeconds delay = new WaitForSeconds(spawnRate);
            bool toggle = false;
            while (true)
            {
                SpawnHelicopter(toggle);
                yield return delay;
                toggle = !toggle;
            }
        }

        public void SpawnHelicopter(bool toggle)
        {
            if (isTroopersCollected) return;

            HelicopterController helicopter = helicopterPool.GetHelicopter();
            helicopter.SetPosition(toggle);
            helicopter.ChangeVisibilityState(true);
        }

        public void ReturnHelicopterToPool(HelicopterController controller)
        {
            helicopterPool.ReturnToPool(controller);
            controller.ChangeVisibilityState(false);
        }

        public void ReturnTrooperToPool(TrooperController trooperToReturn)
        {
            trooperPool.ReturnTrooperToPool(trooperToReturn);
            trooperToReturn.ChangeVisibilityState(false);
        }

        public void StopSpawning() => isTroopersCollected = true;

        public async void CheckActiveParatrooper()
        {
            await helicopterPool.CheckForActiveHelicopter();
            await trooperPool.CheckIsAnyTrooperLeftToReach();
            Debug.Log("calling trooper attack queue");
            GameService.Instance.EventService.OnTrooperAttackTrigger.InvokeEvent();
        }

    }

    //[Serializable]
    //public class HelicopterServiceData
    //{
    //    public HelicopterView HelicopterPrefab;
    //    public HelicopterScriptableObject HelicopterSO;
    //    public Transform LeftSpawnLocation;
    //    public Transform RightSpawnLocation;
    //}

    [Serializable]
    public class AttackableTrooperServiceData
    {
        public AttackableTrooperDetector attackableTrooperDetector;
        public Transform PlayerPosition;
        public Transform RightSideGroundedTrooperParent;
        public Transform LeftSideGroundedTrooperParent;
        public int StepsToClimbByTroopers;
    }
}
