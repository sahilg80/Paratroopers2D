using Assets.Scripts.Main;
using Assets.Scripts.Troopers;
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    public class HelicopterController
    {
        private HelicopterView helicopterView;
        private HelicopterScriptableObject helicopterSO;
        private Transform leftSpawnLocation;
        private Transform rightSpawnLocation;
        private TrooperPool trooperPool;

        public HelicopterController(HelicopterView helicopterPrefab, HelicopterScriptableObject helicopterScriptableObject,
            Transform leftSpawnLocation, Transform rightSpawnLocation, TrooperPool trooperPool)
        {
            helicopterSO = helicopterScriptableObject;
            this.leftSpawnLocation = leftSpawnLocation;
            this.rightSpawnLocation = rightSpawnLocation;
            this.trooperPool = trooperPool;
            helicopterView = Object.Instantiate(helicopterPrefab);
            helicopterView.SetSpeed(helicopterSO.Speed);
            helicopterView.SetController(this);
            helicopterView.SubscribeEvents();
        }

        public void SetPosition(bool isRight)
        {
            if (isRight)
            {
                helicopterView.SetDirectionToMove(rightSpawnLocation);
                helicopterView.transform.position = rightSpawnLocation.position;
                FlipSprite(false);
            }
            else
            {
                helicopterView.SetDirectionToMove(leftSpawnLocation);
                helicopterView.transform.position = leftSpawnLocation.position;
                FlipSprite(true);
            }
        }

        public void ChangeVisibilityState(bool value) => helicopterView.gameObject.SetActive(value);

        private void FlipSprite(bool value) => helicopterView.FlipSprite(value);
        
        public void DeactivateHelicopter() => GameService.Instance.HelicopterService.ReturnHelicopterToPool(this);

        public void OnAttackedByBullet()
        {
            GameService.Instance.EventService.OnParaTrooperKilled.InvokeEvent(helicopterSO.KillReward);
            helicopterView.DisableHelicopter();
        }

        public void SpawnTrooper()
        {
            TrooperController trooperController = trooperPool.GetTrooper();
            trooperController.SetPosition(helicopterView.GetSpawnPosition());
        }

        public void SubscribeEvents()
        {
            GameService.Instance.EventService.OnRequiredTroopersCollected.AddListener(helicopterView.StopSpawningTrooper);
        }
        public void UnSubscribeEvents()
        {
            GameService.Instance.EventService.OnRequiredTroopersCollected.RemoveListener(helicopterView.StopSpawningTrooper);
        }

    }
}
