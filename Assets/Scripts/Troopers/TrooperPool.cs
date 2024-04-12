using Assets.Scripts.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Troopers
{
    public class TrooperPool : GenericObjectPool<TrooperController>
    {
        private TrooperView trooperView;
        private TrooperScriptableObject trooperSO;

        public TrooperPool(TrooperView trooperPrefab, TrooperScriptableObject trooperScriptableObject)
        {
            trooperView = trooperPrefab;
            trooperSO = trooperScriptableObject;
        }

        private List<PooledItem<TrooperController>> GetInUseTroopersList()
        {
            List<PooledItem<TrooperController>> inUseTroopersList = new List<PooledItem<TrooperController>>();
            foreach(PooledItem<TrooperController> item in pooledItems)
            {
                if (item.IsInUse)
                    inUseTroopersList.Add(item);
            }
            return inUseTroopersList;
        }

        public async Task CheckIsAnyTrooperLeftToReach()
        {
            List<PooledItem<TrooperController>> inUseTroopersList = GetInUseTroopersList();
            foreach (var item in inUseTroopersList)
            {
                Debug.Log("collecting remaining troop");
                while (item.Item.GetActiveState() == StateMachine.Troopers.TrooperState.FREEFALL)
                {
                    Debug.Log("deactivating troop");
                    await Task.Delay(1000);
                }
                //if (item.GetActiveState() == StateMachine.Troopers.TrooperState.FREEFALL)
                //    return;
                await Task.Yield();
            }
            Debug.Log("done of trooper");
            await Task.Delay(500);
        }

        public TrooperController GetTrooper()
        {
            Debug.Log("getting trooper");
            return GetItem();
        }

        public void ReturnTrooperToPool(TrooperController controller) => ReturnItem(controller);

        protected override TrooperController CreateItem() => new TrooperController(trooperView, trooperSO);
    }
}