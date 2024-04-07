using Assets.Scripts.Utilities;


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

        public TrooperController GetTrooper() => GetItem();

        public void ReturnTrooperToPool(TrooperController controller) => ReturnItem(controller);

        protected override TrooperController CreateItem() => new TrooperController(trooperView, trooperSO);
    }
}