using DenizYanar.Core;
using DenizYanar.PlayerSystem;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    [RequireComponent(typeof(CrawlerBehaviour))]
    public class Crawler : Enemy
    {
        private Rigidbody2D _rb;
        private CrawlerBehaviour _behaviour;

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

        protected override void OnDeath(Damage damage)
        {
            Destroy(gameObject);
        }

        protected override void OnTakeDamage(Damage damage)
        {
            
        }

        private void Jump()
        {
            var p = GameObject.FindObjectOfType<Player>();

            Vector2 dir = p.transform.position - transform.position;
            dir = Vector2.ClampMagnitude(dir, 10f);
            
            var jumpDirection = new Vector2(dir.normalized.x * 20f, 15f);
            
            _rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        }
    }
}