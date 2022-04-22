using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class BoolEventListener : EventListener
    {
        [SerializeField] [Required]
        private BoolEventChannelSO m_EventChannel;

        [SerializeField]
        private UnityEvent<bool> m_UnityEvent;

        private void Awake() => m_EventChannel.Register(this);

        private void OnDisable() => m_EventChannel.Deregister(this);

        public void RaiseEvent(bool value) => m_UnityEvent.Invoke(value);

    }
}
