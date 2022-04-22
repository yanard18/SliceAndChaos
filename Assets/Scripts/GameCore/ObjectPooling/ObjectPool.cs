using UnityEngine;
using System;
using System.Collections.Generic;



namespace DenizYanar.Pool
{
    public class ObjectPool<T>
    {
        private readonly Queue<T> m_TAvailableObjects = new ();

        private readonly Func<T> m_Spawn;
        private readonly Action<T> m_OnGet;
        private readonly Action<T> m_OnRelease;
        private readonly Action<T> m_OnInit;

        public Transform RootObject { get; }

        public ObjectPool(Func<T> spawn, Action<T> onGet = null, Action<T> onRelease = null, Action<T> onInit = null)
        {
            m_Spawn = spawn;
            m_OnGet = onGet;
            m_OnRelease = onRelease;
            m_OnInit = onInit;

            RootObject = new GameObject("Sound Emitter Pool").transform;
        }


        public T Get()
        {
            T obj;

            if (m_TAvailableObjects.Count > 0)
            {
                obj = m_TAvailableObjects.Dequeue();
                m_OnGet?.Invoke(obj);
                return obj;
            }

            obj = m_Spawn();
            m_OnInit?.Invoke(obj);
            m_OnGet?.Invoke(obj);
            return obj;

        }

        public void Release(T obj)
        {
            m_TAvailableObjects.Enqueue(obj);
            m_OnRelease?.Invoke(obj);
        }

        
    }  
}
