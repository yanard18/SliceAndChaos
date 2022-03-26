using DenizYanar.Core;
using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar.Player
{
    public class Player : Character, IDamageable
    {
        [SerializeField] private SenseEnginePlayer _deathSense;
        [SerializeField] private VoidEventChannelSO _gameOverEvent;
        
        public void TakeDamage(Damage damage)
        {
            Death();
        }

        public override void Death()
        {
            if(IsDeath) return;
            
            _deathSense.Play();
            _gameOverEvent.Invoke();
            
            IsDeath = true;
            Destroy(this.gameObject);
        }
    }
}
