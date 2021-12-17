using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    /// <summary>
    /// This class is used for Events that have int arguments. {Example: Set gold game event}
    /// </summary>

    [CreateAssetMenu(menuName = "Deniz Yanar/Events/Int Event Channel")]
    public class IntEventChannelSO : ScriptableObject
    {
        private HashSet<IntEventListenerSO> _listeners = new HashSet<IntEventListenerSO>();

        public void Invoke(int value)
        {
            foreach (IntEventListenerSO listener in _listeners)
                listener.RaiseEvent(value);
        }

        public void Register(IntEventListenerSO listener) => _listeners.Add(listener);

        public void Deregister(IntEventListenerSO listener) => _listeners.Remove(listener);
    }
}
