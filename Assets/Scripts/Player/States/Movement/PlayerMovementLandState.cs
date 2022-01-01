using DenizYanar.Events;
using DenizYanar.FSM;
using JetBrains.Annotations;

namespace DenizYanar
{
    public class PlayerMovementLandState : State
    {
        private readonly JumpData _jumpData;
        
        public PlayerMovementLandState(JumpData jumpData, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
            _jumpData = jumpData;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _jumpData.ResetJumpCount();
        }
    }
}
