using UnityEngine;

namespace DenizYanar
{
    public class Entity : MonoBehaviour
    {
        public string Name = "Default Entity";

        protected bool _isDeath;

        protected void Die()
        {
            _isDeath = true;
            Destroy(gameObject);
        }
    }
}
