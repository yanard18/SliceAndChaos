using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DenizYanar
{
    public class StateMachine
    {
        private State _currentState;
        
        private List<State> _states = new List<State>();
        private List<Transition> _anyTransitions = new List<Transition>();


        public void Tick()
        {
            Transition transition = GetTriggeredTransition();
            if(transition != null)
                SetState(transition.To);
            
            _currentState.Tick();
        }
        
        public void PhysicsTick() => _currentState?.PhysicsTick();


        public void AddTransition(State from, State to, Func<bool> condition)
        {
            from.Transitions.Add(new Transition(to, condition));
        }

        private void SetState(State state)
        {
            if (state == _currentState)
                return;

            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }

        public void InitState(State state)
        {
            _currentState = state;
            _currentState.OnEnter();
        }

        private Transition GetTriggeredTransition()
        {
            //Any transitions has priority
            foreach (Transition anyTransition in _anyTransitions.Where(anyTransition => anyTransition.Condition()))
                return anyTransition;

            
            foreach (Transition transition in _currentState.Transitions)
                if(transition.Condition())
                    return transition;

            return null;
        }

    }
}
