using Assets.Scripts.Bullet;
using Assets.Scripts.Helicopters;
using Assets.Scripts.Player;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        //[Header("Player")]
        public PlayerService PlayerService { get; private set; }

        [Header("Bullet")]
        [SerializeField]
        private BulletView bulletPrefab;
        [SerializeField]
        private BulletScriptableObject bulletSO;

        [Header("Helicopter")]
        [SerializeField]
        private HelicopterScriptableObject helicopterScriptableObject;
        [SerializeField]
        private HelicopterView helicopterPrefab;
        [SerializeField]
        private Transform leftSpawnLocation;
        [SerializeField]
        private Transform rightSpawnLocation;
        public HelicopterService HelicopterService { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            HelicopterService = new HelicopterService(helicopterPrefab, helicopterScriptableObject, leftSpawnLocation, rightSpawnLocation);
            PlayerService = new PlayerService(bulletPrefab, bulletSO);
            StartCoroutine(HelicopterService.RecurringCall());
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}