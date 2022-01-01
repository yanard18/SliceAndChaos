using System;
using System.Collections.Generic;
using System.Linq;

namespace DenizYanar.FSM
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        private List<State> _states = new List<State>();
        private List<Transition> _anyTransitions = new List<Transition>();

        

        public void Tick()
        {
            Transition transition = GetTriggeredTransition();
            if(transition != null)
                SetState(transition.To);
            
            CurrentState.Tick();
        }
        
        public void PhysicsTick() => CurrentState?.PhysicsTick();


        public void AddTransition(State from, State to, Func<bool> condition)
        {
            from.Transitions.Add(new Transition(to, condition));
        }

        public void AddAnyTransition(State to, Func<bool> condition)
        {
            _anyTransitions.Add(new Transition(to, condition));
        }

        private void SetState(State state)
        {
            if (state == CurrentState)
                return;

            CurrentState.OnExit();
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public void InitState(State state)
        {
            CurrentState = state;
            CurrentState.OnEnter();
        }

        private Transition GetTriggeredTransition()
        {
            //Any transitions has priority
            foreach (Transition anyTransition in _anyTransitions.Where(anyTransition => anyTransition.Condition()))
                return anyTransition;

            
            foreach (Transition transition in CurrentState.Transitions)
                if(transition.Condition())
                    return transition;

            return null;
        }

    }
}
