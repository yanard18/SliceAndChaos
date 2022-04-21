using DenizYanar.DamageAndHealthSystem;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    
    [RequireComponent(typeof(Health))]
    public abstract class Enemy : MonoBehaviour
    {
        private Health m_Health;

        protected virtual void Awake()
        {
            m_Health = GetComponent<Health>();
        }
        
        protected virtual void OnEnable()
        {
            m_Health.e_OnDamage += EOnTakeDamage;
            m_Health.e_OnDeath += EOnDeath;
        }

        protected virtual void OnDisable()
        {
            m_Health.e_OnDamage -= EOnTakeDamage;
            m_Health.e_OnDeath -= EOnDeath;
        }

        protected virtual void LoadSettings(EnemySettings settings)
        {
            GetComponent<Health>().SetupHealth(settings.m_Health);
        }



        protected abstract void EOnDeath(Damage damage);

        protected abstract void EOnTakeDamage(Damage damage);
    }
}
