using DenizYanar.DamageAndHealthSystem;
using DenizYanar.Managers;
using DenizYanar.SenseEngine;
using DenizYanar.Singletons;
using UnityEngine;

namespace DenizYanar.PlayerSystem
{
    [RequireComponent(typeof(Health))]
    public class Player : SingletonWithoutCreation<Player>
    {
        [SerializeField]
        private SenseEnginePlayer m_sepDeath;

        private Health m_Health;

        protected override void Awake()
        {
            base.Awake();
            m_Health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            m_Health.e_OnDeath += Death;
        }

        private void OnDisable()
        {
            m_Health.e_OnDeath -= Death;
        }

        
        

        private void Death(Damage damage)
        {
            Debug.Log("Player killed by " + damage.m_Author.name);
            m_sepDeath.Play();
            GameManager.s_Instance.GameOver();
            Destroy(gameObject);
        }
    }

    public static class PlayerUtils
    {
        public static bool IsPlayerExist() => Player.s_Instance != null;
    }
}
