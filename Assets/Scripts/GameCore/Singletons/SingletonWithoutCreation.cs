using UnityEngine;

namespace DenizYanar.Singletons
{
    public class SingletonWithoutCreation<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T s_PrivateInstance;
        public static T s_Instance => s_PrivateInstance;

        protected virtual void Awake()
        {
            if (s_PrivateInstance != null)
            {
                Destroy(gameObject);
                return;
            }

            s_PrivateInstance = GetComponent<T>();
        }
    }

}
