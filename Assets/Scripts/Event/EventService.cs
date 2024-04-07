using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Event
{
    public class EventService
    {
        public EventController OnStartGame { get; private set; }
        public EventController<int> OnParaTrooperKilled { get; private set; }
        public EventService()
        {
            OnStartGame = new EventController();
            OnParaTrooperKilled = new EventController<int>();
        }
    }
}
