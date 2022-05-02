using DenizYanar.BehaviourTreeAI;
using DenizYanar.PlayerSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    [RequireComponent(typeof(Soldier))]
    public class SoldierBehaviour : EnemyBehaviour
    {
        private Soldier m_Soldier;

        [SerializeField] [Required]
        private HumanoidDetection m_HumanoidDetection;
        private void Awake()
        {
            m_Soldier = GetComponent<Soldier>();
            SetupTree();
        }

        protected override void SetupTree()
        {
            m_Tree = new BehaviourTree(tickRate: 0.05f);

            var baseSelector = new Selector("Agro Or Wait");
            var agroSequence = new Sequence("Agro");
            var followOrAttackSequence = new Sequence("Follow and Attack");

            var doesKnowWhereIsPlayer = new Leaf("Does Know Where Is Player", DoesKnowWhereIsPlayer);
            
            
            var wait = new Leaf("Wait", Wait);
            var follow = new Leaf("Chase", Follow);
            var attack = new Leaf("Attack", Attack);
            
            followOrAttackSequence.AddChild(follow);
            followOrAttackSequence.AddChild(attack);
            agroSequence.AddChild(doesKnowWhereIsPlayer);
            agroSequence.AddChild(followOrAttackSequence);
            baseSelector.AddChild(agroSequence);
            baseSelector.AddChild(wait);
            
            
            m_Tree.AddChild(baseSelector);
            m_Tree.PrintTree();
            RunTree();
        }

        private static Node.EStatus Wait() => Node.EStatus.SUCCESS;

        private Node.EStatus DoesKnowWhereIsPlayer()
        {
            if (!PlayerUtils.IsPlayerExist()) return Node.EStatus.FAILURE;
            Vector2? targetPosInDetectionRange = m_HumanoidDetection.m_DetectionSensor.Scan();

            return targetPosInDetectionRange.HasValue ? Node.EStatus.SUCCESS : Node.EStatus.FAILURE;
        }

        private Node.EStatus Follow()
        {
            if (!PlayerUtils.IsPlayerExist()) return Node.EStatus.FAILURE;

            Vector2? targetPosInRememberableRange = m_HumanoidDetection.m_RememberedLocationSensor.Scan();
            
            if (!targetPosInRememberableRange.HasValue) return Node.EStatus.FAILURE;
            
            m_Soldier.ChaseTarget(targetPosInRememberableRange.Value);
            
            var bInAttackRange = m_HumanoidDetection.m_AttackRangeSensor.Scan().HasValue;
            return bInAttackRange ? Node.EStatus.SUCCESS : Node.EStatus.RUNNING;
        }

        private Node.EStatus Attack()
        {
            if (!PlayerUtils.IsPlayerExist()) return Node.EStatus.FAILURE;
            
            Vector2? targetPosInAttackRange = m_HumanoidDetection.m_AttackRangeSensor.Scan();
            var bIsTargetInAttackRange = targetPosInAttackRange.HasValue;
            
            if (!bIsTargetInAttackRange) return Node.EStatus.FAILURE;
            m_Soldier.Attack();
            return Node.EStatus.RUNNING;
        }
    }
}