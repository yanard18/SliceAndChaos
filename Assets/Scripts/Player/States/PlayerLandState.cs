using DenizYanar.Events;
using JetBrains.Annotations;

namespace DenizYanar
{
    public class PlayerLandState : State
    {
        private readonly JumpData _jumpData;
        
        public PlayerLandState(JumpData jumpData, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
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
