using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class VoidEventListener : EventListener
    {
        [SerializeField]
        private VoidEventChannelSO m_EventChannel;

        [SerializeField]
        private UnityEvent m_UnityEvent;

        private void Awake() => m_EventChannel.Register(this);

        private void OnDisable() => m_EventChannel.Deregister(this);

        public void RaiseEvent() => m_UnityEvent.Invoke();
    }
}
