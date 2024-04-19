

namespace Assets.Scripts.Event
{
    public class EventService
    {
        public EventController OnStartGame { get; private set; }
        public EventController<int> OnParaTrooperKilled { get; private set; }
        public EventController OnRequiredTroopersCollected { get; private set; }
        public EventController OnTrooperAttackTrigger { get; private set; }
        public EventController OnPlayerDeath { get; private set; }
        public EventService()
        {
            OnStartGame = new EventController();
            OnParaTrooperKilled = new EventController<int>();
            OnRequiredTroopersCollected = new EventController();
            OnTrooperAttackTrigger = new EventController();
            OnPlayerDeath = new EventController();
        }
    }
}
