using System;
using DenizYanar.BehaviourTreeAI;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    public class CrawlerBehaviour : EnemyBehaviour
    {
        private float _jumpCooldown;
        
        [SerializeField] private CrawlerSettings _settings;

        public event Action OnAttack;

        private void Awake()
        {
            _jumpCooldown = _settings.m_JumpCooldown;
            SetupTree();
        }

        protected override void SetupTree()
        {
            m_Tree = new BehaviourTree();
            var wait = new Leaf("Wait", Wait);
            var attack = new Leaf("Attack", Attack);

            var agro = new Sequence("agro");
            
            agro.AddChild(wait);
            agro.AddChild(attack);
            
            m_Tree.AddChild(agro);
            StartCoroutine(m_Tree.Behave());
        }

        private Node.EStatus Wait()
        {
             _jumpCooldown -= m_Tree.m_TickRate;
            return _jumpCooldown <= 0 ? Node.EStatus.SUCCESS : Node.EStatus.RUNNING;
        }

        private Node.EStatus Attack()
        {
            OnAttack?.Invoke();
            _jumpCooldown = _settings.m_JumpCooldown;
            return Node.EStatus.SUCCESS;
        }

        
    }
}
