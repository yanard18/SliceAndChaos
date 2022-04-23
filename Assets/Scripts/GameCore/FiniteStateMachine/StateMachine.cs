using System;
using System.Collections.Generic;
using System.Linq;

namespace DenizYanar.FSM
{
    public class StateMachine
    {
        private State m_CurrentState;

        private readonly List<Transition> m_TAnyTransitions = new (); 

        

        public void Tick()
        {
            var transition = GetTriggeredTransition();
            if(transition != null)
                SetState(transition.m_To);
            
            m_CurrentState.Tick();
        }
        
        public void PhysicsTick() => m_CurrentState?.PhysicsTick();


        public void AddTransition(State from, State to, Func<bool> condition) => @from.m_TTransitions.Add(new Transition(to, condition));

        public void AddAnyTransition(State to, Func<bool> condition) => m_TAnyTransitions.Add(new Transition(to, condition));

        private void SetState(State state)
        {
            if (state == m_CurrentState)
                return;

            m_CurrentState.OnExit();
            m_CurrentState = state;
            m_CurrentState.OnEnter();
        }

        public void InitState(State state)
        {
            m_CurrentState = state;
            m_CurrentState.OnEnter();
        }

        public bool TriggerState(State state)
        {
            foreach (var unused in m_CurrentState.m_TTransitions.Where(transition => transition.m_To == state))
            {
                SetState(state);
                return true;
            }

            return false;
        }

        private Transition GetTriggeredTransition()
        {
            //Any transitions has priority
            foreach (var anyTransition in m_TAnyTransitions.Where(anyTransition => anyTransition.m_Condition()))
                return anyTransition;

            
            foreach (var transition in m_CurrentState.m_TTransitions)
                if(transition.m_Condition())
                    return transition;

            return null;
        }

    }
}
