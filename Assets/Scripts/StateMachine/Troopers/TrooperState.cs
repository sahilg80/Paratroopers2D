using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine.Troopers
{
    public enum TrooperState
    {
        FREEFALL,        // state when just left the helicopter
        PARACHUTE,       // state when opened parachute
        ONGROUND,      // in this state it will reach to target player
        CLIMB,           // in this state it will reach climb to its destination
        COMPLETED,       // this state shows task of current troop completed and reached it's destination
        DEAD,            // it is the state when trooper dies
    }
}
