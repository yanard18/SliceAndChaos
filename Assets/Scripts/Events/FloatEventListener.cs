using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Events
{
    public class FloatEventListener : EventListener
    {
        [SerializeField]
        private FloatEventChannelSO m_EventChannel;

        [SerializeField]
        private UnityEvent<float> m_UnityEvent;

        private void Awake() => m_EventChannel.Register(this);

        private void OnDisable() => m_EventChannel.Deregister(this);

        public void RaiseEvent(float value) => m_UnityEvent.Invoke(value);

    }
}
