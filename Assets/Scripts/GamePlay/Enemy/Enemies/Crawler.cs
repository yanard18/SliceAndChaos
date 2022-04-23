using DenizYanar.DamageAndHealthSystem;
using DenizYanar.PlayerSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    [RequireComponent(typeof(CrawlerBehaviour))]
    public class Crawler : Enemy
    {
        private Rigidbody2D m_Rb;
        private CrawlerBehaviour m_Behaviour;
        
        [OnStateUpdate("LoadSettings")] [Required]
        [SerializeField] private CrawlerSettings m_Settings;

        protected override void Awake()
        {
            base.Awake();
            m_Behaviour = GetComponent<CrawlerBehaviour>();
            m_Rb = GetComponent<Rigidbody2D>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Behaviour.e_OnAttack += Jump;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_Behaviour.e_OnAttack -= Jump;
        }

        protected override void EOnDeath(Damage damage)
        {
            Destroy(gameObject);
        }

        protected override void EOnTakeDamage(Damage damage)
        {
            
        }

        private void Jump()
        {
            var player = Player.s_Instance;

            Vector2 dir = player.transform.position - transform.position;
            dir = Vector2.ClampMagnitude(dir, 10f);
            
            var jumpDirection = new Vector2(dir.normalized.x * m_Settings.m_HorizontalJumpForce, m_Settings.m_VerticalJumpForce);
            
            m_Rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        }
    }
}