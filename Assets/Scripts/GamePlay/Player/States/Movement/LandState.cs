using DenizYanar.Events;
using DenizYanar.SenseEngine;
using DenizYanar.FSM;
using JetBrains.Annotations;

namespace DenizYanar.PlayerSystem.Movement
{
    public class LandState : State
    {
        private readonly JumpData m_JumpData;
        private readonly SenseEnginePlayer _landSense;

        #region Constructor

        public LandState(JumpData jumpData, SenseEnginePlayer landSense, StringEvent nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            m_StateName = stateName ?? GetType().Name;
            m_ecStateName = nameInformerEvent;
            m_JumpData = jumpData;
            _landSense = landSense;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            m_JumpData.ResetJumpCount();
            _landSense.Play();
        }

        #endregion
        
    }
}
