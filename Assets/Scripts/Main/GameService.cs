using Assets.Scripts.Bullet;
using Assets.Scripts.Event;
using Assets.Scripts.Helicopters;
using Assets.Scripts.Player;
using Assets.Scripts.Troopers;
using Assets.Scripts.UI;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [Header("Player")]
        [SerializeField]
        private PlayerView playerView;
        [SerializeField]
        private PlayerScriptableObject playerSO;
        public PlayerService PlayerService { get; private set; }

        [Header("Bullet")]
        [SerializeField]
        private BulletView bulletPrefab;

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

        [Header("Troopers")]
        [SerializeField]
        private TrooperView trooperView;
        [SerializeField]
        private TrooperScriptableObject trooperSO;

        [Header("UI")]

        [SerializeField] 
        private UIService uiService;
        public UIService UIService { get => uiService; }
        public EventService EventService { get; private set; }


        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            EventService = new EventService();
            HelicopterService = new HelicopterService(helicopterPrefab, helicopterScriptableObject, 
                leftSpawnLocation, rightSpawnLocation, trooperView, trooperSO);
            PlayerService = new PlayerService(bulletPrefab, playerView, playerSO);
        }

        private void OnEnable()
        {
            PlayerService.SubscribeEvents();
            UIService.SubscribeToEvents();
            EventService.OnStartGame.AddListener(OnGameStart);
        }

        private void OnDisable()
        {
            PlayerService.UnSubscribeEvents();
            UIService.UnSubscribeToEvents();
            EventService.OnStartGame.RemoveListener(OnGameStart);
        }

        private void OnGameStart() => StartCoroutine(HelicopterService.UpdateLoop());

    }
}