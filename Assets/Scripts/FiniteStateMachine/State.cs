using System.Collections.Generic;

namespace DenizYanar
{
    public abstract class State
    {
        public readonly List<Transition> Transitions = new List<Transition>();

        public abstract void Tick();

        public abstract void PhysicsTick();

        public abstract void OnEnter();

        public abstract void OnExit();
        
    }
}
