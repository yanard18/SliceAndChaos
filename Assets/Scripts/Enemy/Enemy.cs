using DenizYanar.Core;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    
    [RequireComponent(typeof(Health))]
    public abstract class Enemy : MonoBehaviour
    {
        private Health _health;

        protected virtual void Awake()
        {
            _health = GetComponent<Health>();
        }
        
        protected virtual void OnEnable()
        {
            _health.OnDamage += OnTakeDamage;
            _health.OnDeath += OnDeath;
        }

        protected virtual void OnDisable()
        {
            _health.OnDamage -= OnTakeDamage;
            _health.OnDeath -= OnDeath;
        }

        protected virtual void LoadSettings(EnemySettings settings)
        {
            GetComponent<Health>().SetupHealth(settings.Health);
        }



        protected abstract void OnDeath(Damage damage);

        protected abstract void OnTakeDamage(Damage damage);
    }
}
