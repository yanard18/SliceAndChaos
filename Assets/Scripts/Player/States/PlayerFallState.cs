using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerFallState : State
    {
      
        public PlayerFallState(StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
        }
        
        
    }
}
