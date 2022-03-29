using UnityEngine;

namespace DenizYanar.Core
{
    [RequireComponent(typeof(Health))]
    public abstract class Character : MonoBehaviour
    {
        private Health _health;
        public string Name;
        

        protected virtual void Awake() => _health = GetComponent<Health>();

        protected virtual void OnEnable() => _health.OnDeath += Death;

        protected virtual void OnDisable() => _health.OnDeath -= Death;

        protected abstract void Death(Damage damage);
    }
}
