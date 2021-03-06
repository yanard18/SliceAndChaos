using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class IntEventListener : EventListener
    {
        [SerializeField] [Required]
        private IntEvent m_EventChannel;

        [SerializeField]
        private UnityEvent<int> m_UnityEvent;

        private void Awake() => m_EventChannel.Register(this);

        private void OnDisable() => m_EventChannel.Deregister(this);

        public void RaiseEvent(int value) => m_UnityEvent.Invoke(value);
    }
}