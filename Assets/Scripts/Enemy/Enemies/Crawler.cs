using DenizYanar.DamageAndHealthSystem;
using DenizYanar.PlayerSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    [RequireComponent(typeof(CrawlerBehaviour))]
    public class Crawler : Enemy
    {
        private Rigidbody2D _rb;
        private CrawlerBehaviour _behaviour;
        
        [OnStateUpdate("LoadSettings")]
        [SerializeField] private CrawlerSettings _settings;

        protected override void Awake()
        {
            base.Awake();
            _behaviour = GetComponent<CrawlerBehaviour>();
            _rb = GetComponent<Rigidbody2D>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _behaviour.OnAttack += Jump;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _behaviour.OnAttack -= Jump;
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
            
            var jumpDirection = new Vector2(dir.normalized.x * _settings.m_HorizontalJumpForce, _settings.m_VerticalJumpForce);
            
            _rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        }
    }
}