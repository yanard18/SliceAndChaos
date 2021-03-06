using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    
    [CreateAssetMenu(menuName = "Game Event/String Event")]
    public class StringEvent : ScriptableObject
    {
        private readonly HashSet<StringEventListener> m_TListeners = new();

        public void Invoke(string value)
        {
            foreach (var listener in m_TListeners)
                listener.RaiseEvent(value);
        }

        public void Register(StringEventListener listener) => m_TListeners.Add(listener);

        public void Deregister(StringEventListener listener) => m_TListeners.Remove(listener);
    }
}
