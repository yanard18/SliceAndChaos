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
            _health.e_OnDamage += EOnTakeDamage;
            _health.e_OnDeath += EOnDeath;
        }

        protected virtual void OnDisable()
        {
            _health.e_OnDamage -= EOnTakeDamage;
            _health.e_OnDeath -= EOnDeath;
        }

        protected virtual void LoadSettings(EnemySettings settings)
        {
            GetComponent<Health>().SetupHealth(settings.Health);
        }



        protected abstract void EOnDeath(Damage damage);

        protected abstract void EOnTakeDamage(Damage damage);
    }
}
