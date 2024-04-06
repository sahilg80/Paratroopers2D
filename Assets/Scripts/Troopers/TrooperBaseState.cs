using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Troopers
{
    public abstract class TrooperBaseState
    {
        public abstract void EnterState(TrooperStateMachine stateMachine, Action onSuccess);
        public abstract void UpdateState(TrooperStateMachine stateMachine);
        public abstract void ExitState(TrooperStateMachine stateMachine);
    }
}
