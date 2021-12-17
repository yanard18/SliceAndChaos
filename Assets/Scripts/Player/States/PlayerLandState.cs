using UnityEngine;

namespace DenizYanar
{
    public class PlayerLandState : State
    {
        public override void Tick()
        {
            Debug.Log(this.GetType().Name);
        }

        public override void PhysicsTick()
        {
            
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}
