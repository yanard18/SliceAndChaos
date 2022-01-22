using DenizYanar.Events;
using DenizYanar.FSM;
using JetBrains.Annotations;

namespace DenizYanar.Player
{
    public class PlayerMovementLandState : State
    {
        private readonly JumpData _jumpData;

        #region Constructor

        public PlayerMovementLandState(JumpData jumpData, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
            _jumpData = jumpData;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            _jumpData.ResetJumpCount();
        }

        #endregion
        
    }
}
