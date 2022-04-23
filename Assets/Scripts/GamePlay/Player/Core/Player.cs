using DenizYanar.DamageAndHealthSystem;
using DenizYanar.Events;
using DenizYanar.SenseEngine;
using DenizYanar.Singletons;
using UnityEngine;

namespace DenizYanar.PlayerSystem
{
    [RequireComponent(typeof(Health))]
    public class Player : Singleton<Player>
    {
        [SerializeField] private SenseEnginePlayer _deathSense;
        [SerializeField] private VoidEvent _gameOverEvent;

        private Health _health;


        protected override void Awake()
        {
            base.Awake();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.e_OnDeath += Death;
        }

        private void OnDisable()
        {
            _health.e_OnDeath -= Death;
        }

        
        

        private void Death(Damage damage)
        {
            Debug.Log("Player killed by " + damage.m_Author.name);
            _deathSense.Play();
            _gameOverEvent.Invoke();
            Destroy(this.gameObject);
        }
    }
}
