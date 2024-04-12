using Assets.Scripts.Troopers;
using Assets.Scripts.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private List<PooledItem<HelicopterController>> GetListOfInUseHelicopters()
        {
            List<PooledItem<HelicopterController>> inUseHelicoptersList = new List<PooledItem<HelicopterController>>();
            foreach (PooledItem<HelicopterController> item in pooledItems)
            {
                if (item.IsInUse)
                    inUseHelicoptersList.Add(item);
            }
            return inUseHelicoptersList;
        }
        
        public async Task CheckForActiveHelicopter()
        {
            List<PooledItem<HelicopterController>> inUseHelicoptersList = GetListOfInUseHelicopters();
            foreach (PooledItem<HelicopterController> item in inUseHelicoptersList)
            {
                Debug.Log("collecting remaining helicopter");
                while (item.IsInUse)
                {
                    Debug.Log("deactivating helicopter");
                    await Task.Delay(1000);
                }
                await Task.Yield();
            }
            Debug.Log("done of helicopter");
            await Task.Delay(500);
        }

        public HelicopterController GetHelicopter() => GetItem();

        public void ReturnToPool(HelicopterController controller) => ReturnItem(controller);

        protected override HelicopterController CreateItem() => new HelicopterController(helicopterPrefab, helicopterSO, leftSpawnLocation, rightSpawnLocation, trooperPool);
    }
}