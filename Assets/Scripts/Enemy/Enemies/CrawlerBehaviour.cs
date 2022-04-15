using DenizYanar.BehaviourTreeAI;
using DenizYanar.Core;
using UnityEngine;

namespace DenizYanar
{
    public class CrawlerBehaviour : MonoBehaviour
    {
        private BehaviourTree _tree;
        private float _waitCooldown = 3.0f;

        protected void Awake()
        {
            _tree = new BehaviourTree();
            var wait = new Leaf("Wait", Wait);
            var attack = new Leaf("Attack", Attack);

            var agro = new Sequence("agro");
            
            agro.AddChild(wait);
            agro.AddChild(attack);
            
            _tree.AddChild(agro);
            StartCoroutine(_tree.Behave());
        }

        private Node.EStatus Wait()
        {
            _waitCooldown -= _tree.TickRate;
            return _waitCooldown <= 0 ? Node.EStatus.SUCCESS : Node.EStatus.RUNNING;
        }

        private Node.EStatus Attack()
        {
            _waitCooldown = 3.0f;
            return Node.EStatus.SUCCESS;
        }
        
    }
}
