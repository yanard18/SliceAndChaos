using System;
using DenizYanar.BehaviourTreeAI;

namespace DenizYanar.EnemySystem
{
    public class CrawlerBehaviour : EnemyBehaviour
    {
        private float _waitCooldown = 3.0f;

        public event Action OnAttack;

        private void Awake()
        {
            SetupTree();
        }

        protected override void SetupTree()
        {
            Tree = new BehaviourTree();
            var wait = new Leaf("Wait", Wait);
            var attack = new Leaf("Attack", Attack);

            var agro = new Sequence("agro");
            
            agro.AddChild(wait);
            agro.AddChild(attack);
            
            Tree.AddChild(agro);
            StartCoroutine(Tree.Behave());
        }

        private Node.EStatus Wait()
        {
            _waitCooldown -= Tree.TickRate;
            return _waitCooldown <= 0 ? Node.EStatus.SUCCESS : Node.EStatus.RUNNING;
        }

        private Node.EStatus Attack()
        {
            OnAttack?.Invoke();
            _waitCooldown = 3.0f;
            return Node.EStatus.SUCCESS;
        }

        
    }
}
