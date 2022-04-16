using System;
using DenizYanar.Core;
using UnityEngine;

namespace DenizYanar.BodySystem
{
    
    [RequireComponent(typeof(Health))]
    public class BodyPart : MonoBehaviour
    {
        public event Action<BodyPart, Damage> OnDestroyed;
        
        public string PartName;
        public bool IsRoot;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnDamage += OnTakeDamage;
            _health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            _health.OnDamage -= OnTakeDamage;
            _health.OnDeath -= OnDeath;
        }

        protected virtual void OnTakeDamage(Damage damage)
        {

        }

        protected virtual void OnDeath(Damage damage)
        {
            OnDestroyed?.Invoke(this, damage);
        }
    }
}
