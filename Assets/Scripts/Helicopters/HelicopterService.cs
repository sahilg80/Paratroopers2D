using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    public class HelicopterService
    {
        private HelicopterPool helicopterPool;
        private const float spawnRate = 1.5f;

        public HelicopterService(HelicopterView helicopterPrefab, HelicopterScriptableObject helicopterSO,
            Transform leftSpawnLocation, Transform rightSpawnLocation)
        {
            helicopterPool = new HelicopterPool(helicopterPrefab, helicopterSO, leftSpawnLocation, rightSpawnLocation);
        }

        // spawn helicopter after every fixed interval of time
        // helicopter will be spawned in alternate way from left to right
        public IEnumerator RecurringCall()
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
            HelicopterController helicopter = helicopterPool.GetHelicopter();
            helicopter.SetPosition(toggle);
            helicopter.ChangeVisibilityState(true);
        }

        public void ReturnHelicopterToPool(HelicopterController controller)
        {
            helicopterPool.ReturnToPool(controller);
            controller.ChangeVisibilityState(false);
        }

    }
}
