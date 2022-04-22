using System;
using System.Collections;
using DenizYanar.SenseEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.DamageAndHealthSystem
{
    public class Health : MonoBehaviour
    {
        private bool m_bHasImmunity;

        [Header("Health Configurations")]
        [Range(0, 9999)] [SerializeField] private float m_MaxHealth = 100.0f;
        [ProgressBar(0,1000)] [SerializeField] private float m_Health = 100.0f;
        [Range(0, 5)] [SerializeField] private float m_ImmunityDuration;
        
        [Header("Sense Players")]
        [SerializeField] private SenseEnginePlayer m_sepDamage;
        [SerializeField] private SenseEnginePlayer m_sepDeath;

        public event Action<Damage> e_OnDeath;
        public event Action<Damage> e_OnDamage;



        public void SetupHealth(float maxHealth)
        {
            m_MaxHealth = maxHealth;
            m_Health = maxHealth;
        }
        
        public void TakeDamage(Damage damage)
        {
            if(AlreadyDeath) return;
            if(m_bHasImmunity) return;

            m_Health -= damage.m_DamageValue;
            e_OnDamage?.Invoke(damage);
            m_sepDamage.PlayIfExist();
            
            if (HasImmunityAbility) StartCoroutine(StartImmunity(m_ImmunityDuration));
            if(IsHealthLessThanZero) Death(damage);
        }

        private void Death(Damage damage)
        {
            m_sepDeath.PlayIfExist();
            e_OnDeath?.Invoke(damage);
        }

        public void RestoreHealth(float value)
        {
            m_Health += Mathf.Abs(value);
            m_Health = Mathf.Clamp(m_Health, 0, m_MaxHealth);
        }

        private IEnumerator StartImmunity(float duration)
        {
            m_bHasImmunity = true;
            yield return new WaitForSeconds(duration);
            m_bHasImmunity = false;
        }
        
        private bool IsHealthLessThanZero => m_Health <= 0;

        private bool HasImmunityAbility => m_ImmunityDuration > 0;
        private bool AlreadyDeath => m_Health <= 0;
    }
}
