using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    /// <summary>
    /// This class is used for Events that have bool arguments. {Example: Set player blink availability game event}
    /// </summary>

    [CreateAssetMenu(menuName = "Slice And Chaos/Events/Bool Event Channel")]
    public class BoolEventChannelSO : ScriptableObject
    {
        private readonly HashSet<BoolEventListener> m_TListeners = new();

        public void Invoke(bool value)
        {
            foreach (var listener in m_TListeners)
                listener.RaiseEvent(value);
        }

        public void Register(BoolEventListener listener) => m_TListeners.Add(listener);

        public void Deregister(BoolEventListener listener) => m_TListeners.Remove(listener);
    }
}
