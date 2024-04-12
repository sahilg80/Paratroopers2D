using Assets.Scripts.Helicopters;
using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers.AttackableTroopers
{
    public class AttackableTrooperService
    {
        //private Transform groundedTrooperParent;
        //private Transform playerPosition;
        private AttackableTrooperController attackableTrooperController;
        //private TrooperPool trooperPool;

        public AttackableTrooperService(AttackableTrooperServiceData attackableTrooperServiceData)
        {
            //this.groundedTrooperParent = groundedTrooperParent;
            //this.playerPosition = playerPosition;
            //GetTrooperPool();
            attackableTrooperController = new AttackableTrooperController(attackableTrooperServiceData);
        }

        public void UpdateLoop()
        {
            attackableTrooperController.UpdateLoop();
        }

        public void SubscribeEvents()
        {
            GameService.Instance.EventService.OnTrooperAttackTrigger.AddListener(StartPreparingAttackToPlayer);
        }

        public void UnSubscribeEvents()
        {
            GameService.Instance.EventService.OnTrooperAttackTrigger.RemoveListener(StartPreparingAttackToPlayer);
        }

        private void StartPreparingAttackToPlayer()
        {
            attackableTrooperController.CreateTrooperAttackQueue();
        }

        //private void GetTrooperPool() => trooperPool = GameService.Instance.HelicopterService.GetTroopePool();
        

    }
}
