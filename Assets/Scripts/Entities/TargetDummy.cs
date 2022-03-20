using DenizYanar.Core;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using DenizYanar.External.Sense_Engine.Scripts.Senses;
using UnityEngine;

namespace DenizYanar
{
    public class TargetDummy : Entity, IDamageable
    {
        [SerializeField] private SenseEnginePlayer _deathSense;

        public void TakeDamage(Damage damage)
        {
            if(_isDeath) return;
            
            ConfigureAndPlayDeathSense(damage);
            Die();
        }

        private void ConfigureAndPlayDeathSense(Damage damage)
        {
            var dir = damage.Author.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rot = Quaternion.Euler(angle, -90, 90);
            _deathSense.GetComponent<SenseInstantiateObject>().InstantiateRotation = rot;
            _deathSense.Play();
        }
    }
}