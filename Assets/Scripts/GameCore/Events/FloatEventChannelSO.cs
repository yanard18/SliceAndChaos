using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    /// <summary>
    /// This class is used for Events that have float arguments. {Example: Set player health game event}
    /// </summary>

    [CreateAssetMenu(menuName = "Slice And Chaos/Events/Float Event Channel")]
    public class FloatEventChannelSO : ScriptableObject
    {
        private readonly HashSet<FloatEventListener> m_TListeners = new(); 

        public void Invoke(float value)
        {
            foreach (var listener in m_TListeners)
                listener.RaiseEvent(value);
        }

        public void Register(FloatEventListener listener) => m_TListeners.Add(listener);

        public void Deregister(FloatEventListener listener) => m_TListeners.Remove(listener);
    }
}
