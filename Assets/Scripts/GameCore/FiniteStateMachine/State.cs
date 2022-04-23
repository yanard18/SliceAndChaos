using System.Collections.Generic;
using DenizYanar.Events;

namespace DenizYanar.FSM
{
    public class State
    {
        public readonly List<Transition> Transitions = new List<Transition>();

        protected StringEvent _stateNameInformerEventChannel;
        protected string _stateName;

        public virtual void Tick()
        {
            
        }

        public virtual void PhysicsTick()
        {
            
        }

        public virtual void OnEnter()
        {
            if(_stateNameInformerEventChannel != null)
                _stateNameInformerEventChannel.Invoke(_stateName);
        }

        public virtual void OnExit()
        {
            
        }
        
    }
}
