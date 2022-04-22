using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    /// <summary>
    /// This class is used for Events that have no arguments. {Example: Exit game event}
    /// </summary>

    [CreateAssetMenu(menuName = "Slice And Chaos/Events/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject
    { 
        private readonly HashSet<VoidEventListener> m_Listeners = new();     

        public void Invoke()
        {
            foreach (var listener in m_Listeners)
                listener.RaiseEvent();
        }

        public void Register(VoidEventListener listener) => m_Listeners.Add(listener);

        public void Deregister(VoidEventListener listener) => m_Listeners.Remove(listener);
    }
}
