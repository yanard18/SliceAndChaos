using DenizYanar.BehaviourTreeAI;
using DenizYanar.Core;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using DenizYanar.External.Sense_Engine.Scripts.Senses;
using DenizYanar.PlayerSystem;
using UnityEngine;

namespace DenizYanar
{
    public class TargetDummy : Agent
    {
        [SerializeField] private SenseEnginePlayer _deathSense;

        protected override void Awake()
        {
            base.Awake();
            
            var wait = new Leaf("Waiting Like A Dummy!", JustWait);
            var go = new Leaf("GO", GoToPlayer);
            var act = new Sequence("Action");
            
            act.AddChild(wait);
            act.AddChild(go);
            
            Tree.AddChild(act);
        
        }

        private void ConfigureAndPlayDeathSense(Damage damage)
        {
            var dir = damage.Author.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rot = Quaternion.Euler(angle, -90, 90);
            _deathSense.GetComponent<SenseInstantiateObject>().InstantiateRotation = rot;
            _deathSense.Play();
        }
        
        private Node.EStatus JustWait() => Node.EStatus.SUCCESS;

        private Node.EStatus GoToPlayer()
        {
            var destination = (Vector2)FindObjectOfType<Player>().transform.position;
            var distanceToTarget = Vector2.Distance(transform.position, destination);
            var dir = destination - (Vector2)transform.position;
            dir.Normalize();
            if (distanceToTarget >= 1f)
            {
                transform.Translate(dir);
                return Node.EStatus.RUNNING;
            }

            return Node.EStatus.SUCCESS;


        }

        protected override void Death(Damage damage)
        {
            ConfigureAndPlayDeathSense(damage);
            Destroy(gameObject);
        }
    }
}
