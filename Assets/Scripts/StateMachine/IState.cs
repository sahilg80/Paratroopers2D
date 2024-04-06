using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public interface IState
    {
        void OnEnterState();
        void UpdateState();
        void OnExitState();
    }
}
