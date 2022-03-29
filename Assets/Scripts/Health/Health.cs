using System;
using System.Collections;
using UnityEngine;

namespace DenizYanar.Core
{
    public class Health : MonoBehaviour
    {
        public event Action<Damage> OnDeath;

        [Range(0, 9999)] [SerializeField] private float _health = 100.0f;
        [Range(0, 9999)] [SerializeField] private float _maxHealth = 100.0f;
        [SerializeField] private float _immunityDuration;
        
        private bool _hasImmunity;



        public void TakeDamage(Damage damage)
        {
            if(AlreadyDeath) return;
            if(_hasImmunity) return;

            _health -= damage.DamageValue;
            if (HasImmunityAbility) StartCoroutine(StartImmunity(_immunityDuration));
            if(IsHealthLessThanZero) OnDeath?.Invoke(damage);
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
