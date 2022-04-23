using System.Collections.Generic;
using DenizYanar.Events;

namespace DenizYanar.FSM
{
    public class State
    {
        public readonly List<Transition> m_TTransitions = new();

        protected StringEvent m_ecStateName;
        protected string m_StateName;

        public virtual void Tick()
        {
            
        }

        public virtual void PhysicsTick()
        {
            
        }

        public virtual void OnEnter()
        {
            if(m_ecStateName != null)
                m_ecStateName.Invoke(m_StateName);
        }

        public virtual void OnExit()
        {
            
        }
        
    }
}
