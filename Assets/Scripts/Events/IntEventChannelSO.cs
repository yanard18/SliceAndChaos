using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    /// <summary>
    /// This class is used for Events that have int arguments. {Example: Set gold game event}
    /// </summary>

    [CreateAssetMenu(menuName = "Slice And Chaos/Events/Int Event Channel")]
    public class IntEventChannelSO : ScriptableObject
    {
        private readonly HashSet<IntEventListener> m_TListeners = new();

        public void Invoke(int value)
        {
            foreach (var listener in m_TListeners)
                listener.RaiseEvent(value);
        }

        public void Register(IntEventListener listener) => m_TListeners.Add(listener);

        public void Deregister(IntEventListener listener) => m_TListeners.Remove(listener);
    }
}
