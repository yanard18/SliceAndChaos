using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using DenizYanar.FSM;
using JetBrains.Annotations;

namespace DenizYanar.PlayerSystem.Movement
{
    public class LandState : State
    {
        private readonly JumpData _jumpData;
        private readonly SenseEnginePlayer _landSense;

        #region Constructor

        public LandState(JumpData jumpData, SenseEnginePlayer landSense, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
            _jumpData = jumpData;
            _landSense = landSense;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            _jumpData.ResetJumpCount();
            _landSense.Play();
        }

        #endregion
        
    }
}
