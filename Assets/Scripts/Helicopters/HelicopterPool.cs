using Assets.Scripts.Troopers;
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
        private TrooperPool trooperPool;

        public HelicopterPool(HelicopterView helicopterView, HelicopterScriptableObject helicopterScriptableObject,
            Transform leftSpawnLocation, Transform rightSpawnLocation, TrooperPool trooperPool)
        {
            helicopterPrefab = helicopterView;
            helicopterSO = helicopterScriptableObject;
            this.leftSpawnLocation = leftSpawnLocation;
            this.rightSpawnLocation = rightSpawnLocation;
            this.trooperPool = trooperPool;
        }

        public HelicopterController GetHelicopter() => GetItem();

        public void ReturnToPool(HelicopterController controller) => ReturnItem(controller);

        protected override HelicopterController CreateItem() => new HelicopterController(helicopterPrefab, helicopterSO, leftSpawnLocation, rightSpawnLocation, trooperPool);
    }
}