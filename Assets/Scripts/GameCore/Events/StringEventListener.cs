using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace DenizYanar.Events
{
    public class StringEventListener : EventListener
    {
        [Required] [SerializeField]
        private StringEventChannelSO m_EventChannel;

        [SerializeField] private UnityEvent<string> m_UnityEvent;
        
        private void Awake() => m_EventChannel.Register(this);

        private void OnDisable() => m_EventChannel.Deregister(this);

        public void RaiseEvent(string value) => m_UnityEvent.Invoke(value);
    }
}