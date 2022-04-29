using System;
using DenizYanar.BehaviourTreeAI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    public class CrawlerBehaviour : EnemyBehaviour
    {
        private float m_JumpCooldown;
        
        [SerializeField] [Required]
        private CrawlerSettings m_Settings;

        public event Action e_OnAttack;

        private void Awake()
        {
            m_JumpCooldown = m_Settings.m_JumpCooldown;
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
            RunTree();
        }

        private Node.EStatus Wait()
        {
             m_JumpCooldown -= m_Tree.m_TickRate;
            return m_JumpCooldown <= 0 ? Node.EStatus.SUCCESS : Node.EStatus.RUNNING;
        }

        private Node.EStatus Attack()
        {
            e_OnAttack?.Invoke();
            m_JumpCooldown = m_Settings.m_JumpCooldown;
            return Node.EStatus.SUCCESS;
        }

        
    }
}
