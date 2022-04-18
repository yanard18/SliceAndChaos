using System;
using DenizYanar.Core;
using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using DenizYanar.Singletons;
using UnityEngine;

namespace DenizYanar.PlayerSystem
{
    [RequireComponent(typeof(Health))]
    public class Player : Singleton<Player>
    {
        [SerializeField] private SenseEnginePlayer _deathSense;
        [SerializeField] private VoidEventChannelSO _gameOverEvent;

        private Health _health;


        protected override void Awake()
        {
            base.Awake();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnDeath += Death;
        }

        private void OnDisable()
        {
            _health.OnDeath -= Death;
        }


        private void Death(Damage damage)
        {
            Debug.Log("Player killed by " + damage.Author.name);
            _deathSense.Play();
            _gameOverEvent.Invoke();
            Destroy(this.gameObject);
        }
    }
}
