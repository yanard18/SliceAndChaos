using DenizYanar.Events;
using JetBrains.Annotations;

namespace DenizYanar
{
    public class PlayerRisingState : State
    {
        public PlayerRisingState(StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
        }
        
    }
}
