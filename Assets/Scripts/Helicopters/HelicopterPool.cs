using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Helicopters
{
    public class HelicopterPool : GenericObjectPool<HelicopterController>
    {
        private HelicopterView helicopterPrefab;
        private HelicopterScriptableObject helicopterSO;
        private Transform leftSpawnLocation;
        private Transform rightSpawnLocation;

        public HelicopterPool(HelicopterView helicopterView, HelicopterScriptableObject helicopterScriptableObject,
            Transform leftSpawnLocation, Transform rightSpawnLocation)
        {
            helicopterPrefab = helicopterView;
            helicopterSO = helicopterScriptableObject;
            this.leftSpawnLocation = leftSpawnLocation;
            this.rightSpawnLocation = rightSpawnLocation;
        }

        public HelicopterController GetHelicopter() => GetItem();

        public void ReturnToPool(HelicopterController controller) => ReturnItem(controller);

        protected override HelicopterController CreateItem() => new HelicopterController(helicopterPrefab, helicopterSO, leftSpawnLocation, rightSpawnLocation);
    }
}