using Assets.Scripts.Bullet;
using Assets.Scripts.Event;
using Assets.Scripts.Helicopters;
using Assets.Scripts.Player;
using Assets.Scripts.Troopers;
using Assets.Scripts.Troopers.AttackableTroopers;
using Assets.Scripts.UI;
using Assets.Scripts.Utilities;
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
        private TrooperView trooperPrefab;
        [SerializeField]
        private TrooperScriptableObject trooperScriptableObject;
        [SerializeField]
        private AttackableTrooperServiceData attackableTrooperServiceData;
        private AttackableTrooperService attackableTrooperService;

        [Header("UI")]

        [SerializeField] 
        private UIService uiService;
        public UIService UIService { get => uiService; }
        public EventService EventService { get; private set; }
        private Coroutine helicopterSpawningCoroutine;

        public int NameCounter;
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            EventService = new EventService();
            HelicopterService = new HelicopterService(helicopterPrefab, helicopterScriptableObject, 
                leftSpawnLocation, rightSpawnLocation, trooperPrefab,  trooperScriptableObject);
            PlayerService = new PlayerService(bulletPrefab, playerView, playerSO);
            attackableTrooperService = new AttackableTrooperService(attackableTrooperServiceData);
        }

        private void OnEnable()
        {
            PlayerService.SubscribeEvents();
            UIService.SubscribeToEvents();
            attackableTrooperService.SubscribeEvents();
            EventService.OnStartGame.AddListener(StartHelicopterSpawning);
            EventService.OnRequiredTroopersCollected.AddListener(OnTroopersRequiredCollected);
        }

        private void OnDisable()
        {
            PlayerService.UnSubscribeEvents();
            UIService.UnSubscribeToEvents();
            attackableTrooperService.UnSubscribeEvents();
            EventService.OnStartGame.RemoveListener(StartHelicopterSpawning);
            EventService.OnRequiredTroopersCollected.RemoveListener(OnTroopersRequiredCollected);
        }

        private void StartHelicopterSpawning() => helicopterSpawningCoroutine = StartCoroutine(HelicopterService.UpdateLoop());

        private void OnTroopersRequiredCollected()
        {
            if (helicopterSpawningCoroutine != null)
            {
                Debug.Log("stopping spawning helicopter");
                StopCoroutine(helicopterSpawningCoroutine);
                HelicopterService.StopSpawning();
                HelicopterService.CheckActiveParatrooper();
            }
        }

        private void Update()
        {
            attackableTrooperService.UpdateLoop();
        }

    }
}