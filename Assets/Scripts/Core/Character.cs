using UnityEngine;

namespace DenizYanar.Core
{
    public abstract class Character : MonoBehaviour
    {
        public string Name;

        protected bool IsDeath;

        public abstract void Death();

    }
}
