using DenizYanar.Events;
using DenizYanar.SenseEngine;
using DenizYanar.FSM;
using JetBrains.Annotations;

namespace DenizYanar.PlayerSystem.Movement
{
    public class LandState : State
    {
        private readonly JumpProperties _jumpProperties;
        private readonly SenseEnginePlayer _landSense;

        #region Constructor

        public LandState(JumpProperties jumpProperties, SenseEnginePlayer landSense, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
            _jumpProperties = jumpProperties;
            _landSense = landSense;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            _jumpProperties.ResetJumpCount();
            _landSense.Play();
        }

        #endregion
        
    }
}
