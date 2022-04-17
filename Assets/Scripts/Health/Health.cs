using System;
using System.Collections;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar.Core
{
    public class Health : MonoBehaviour
    {
        private bool _hasImmunity;

        [Header("Health Configurations")]
        [Range(0, 9999)] [SerializeField] private float _health = 100.0f;
        [Range(0, 9999)] [SerializeField] private float _maxHealth = 100.0f;
        [Range(0, 5)] [SerializeField] private float _immunityDuration;
        
        [Header("Sense Players")]
        [SerializeField] private SenseEnginePlayer _damageSense;
        [SerializeField] private SenseEnginePlayer _deathSense;

        public event Action<Damage> OnDeath;
        public event Action<Damage> OnDamage;
        
        

        public void TakeDamage(Damage damage)
        {
            if(AlreadyDeath) return;
            if(_hasImmunity) return;

            _health -= damage.DamageValue;
            OnDamage?.Invoke(damage);
            _damageSense.PlayIfExist();
            
            if (HasImmunityAbility) StartCoroutine(StartImmunity(_immunityDuration));
            if(IsHealthLessThanZero) Death(damage);
        }

        private void Death(Damage damage)
        {
            _deathSense.PlayIfExist();
            OnDeath?.Invoke(damage);
        }

        public void RestoreHealth(float value)
        {
            _health += Mathf.Abs(value);
            _health = Mathf.Clamp(_health, 0, _maxHealth);
        }

        private IEnumerator StartImmunity(float duration)
        {
            _hasImmunity = true;
            yield return new WaitForSeconds(duration);
            _hasImmunity = false;
        }
        
        private bool IsHealthLessThanZero => _health <= 0;

        private bool HasImmunityAbility => _immunityDuration > 0;
        private bool AlreadyDeath => _health <= 0;
    }
}
