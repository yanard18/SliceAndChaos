using UnityEngine;

namespace DenizYanar.Singletons
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T s_PrivateInstance;
        public static T s_Instance
        {
            get
            {
                if (s_PrivateInstance == null)
                {
                    s_PrivateInstance = FindObjectOfType<T>();
                    if (s_PrivateInstance == null)
                    {
                        var singletonObj = new GameObject();
                        singletonObj.name = typeof(T).ToString();
                        s_PrivateInstance = singletonObj.AddComponent<T>();
                    }
                }

                return s_PrivateInstance;
            }
        }

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
