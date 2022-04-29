using DenizYanar.BehaviourTreeAI;
using DenizYanar.PlayerSystem;
using DenizYanar.Sensors;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    [RequireComponent(typeof(Soldier))]
    public class SoldierBehaviour : EnemyBehaviour
    {
        private Soldier m_Soldier;
        
        private ISensor m_DetectionRangeSensor;
        private ISensor m_AttackRangeSensor;


        private void Awake()
        {
            m_Soldier = GetComponent<Soldier>();
            ISensor[] sensors = GetComponents<ISensor>();
            m_DetectionRangeSensor = sensors[0];
            m_AttackRangeSensor = sensors[1];
            SetupTree();
        }

        protected override void SetupTree()
        {
            m_Tree = new BehaviourTree(tickRate: 0.05f);

            var behave = new Sequence("Behave");
            
            
            var wait = new Leaf("Wait", Wait);
            var follow = new Leaf("Chase", Follow);
            var attack = new Leaf("Attack", Attack);
            
            behave.AddChild(wait);
            behave.AddChild(follow);
            behave.AddChild(attack);
            
            m_Tree.AddChild(behave);
            m_Tree.PrintTree();
            RunTree();
        }

        private Node.EStatus Wait()
        {
            Debug.Log("Wait Mode");
            Vector2? targetPos = m_DetectionRangeSensor.Scan();
            return targetPos.HasValue ? Node.EStatus.SUCCESS : Node.EStatus.RUNNING;
        }

        private Node.EStatus Follow()
        {
            Debug.Log("Follow Mode");
            if (!PlayerUtils.IsPlayerExist()) return Node.EStatus.FAILURE;
            Vector2? targetPosInDetectionRange = m_DetectionRangeSensor.Scan();
            
            if (!targetPosInDetectionRange.HasValue) return Node.EStatus.FAILURE;


            m_Soldier.ChaseTarget(targetPosInDetectionRange.Value);
            return m_AttackRangeSensor.Scan().HasValue ? Node.EStatus.SUCCESS : Node.EStatus.RUNNING;
        }

        private Node.EStatus Attack()
        {
            Debug.Log("Attack Mode");
            if (!PlayerUtils.IsPlayerExist()) return Node.EStatus.FAILURE;
            
            Vector2? targetPosInAttackRange = m_AttackRangeSensor.Scan();
            if (!targetPosInAttackRange.HasValue) return Node.EStatus.FAILURE;
            m_Soldier.Attack();
            return Node.EStatus.RUNNING;
        }
    }
}