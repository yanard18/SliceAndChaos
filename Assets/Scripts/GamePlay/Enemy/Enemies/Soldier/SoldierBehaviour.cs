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
            var attackSequence = new Sequence("Attack Sequence");

            
            var doesKnowWhereIsPlayer = new Leaf("Does Know Where Is Player", DoesKnowWhereIsPlayer);
            
            
            var wait = new Leaf("Wait", Wait);
            var follow = new Leaf("Chase", Follow);
            var attack = new Leaf("Attack", Attack);
            var startAttack = new Leaf("Start Attack", StartAttack);
            var stopAttack = new Leaf("Stop Attack", StopAttack);
            
            
            attackSequence.AddChild(startAttack);
            attackSequence.AddChild(attack);
            attackSequence.AddChild(stopAttack);
            followOrAttackSequence.AddChild(follow);
            followOrAttackSequence.AddChild(attackSequence);
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
            var targetInDetectionRange = m_HumanoidDetection.m_DetectionSensor.Scan();

            return targetInDetectionRange != null ? Node.EStatus.SUCCESS : Node.EStatus.FAILURE;
        }

        private Node.EStatus Follow()
        {
            if (!PlayerUtils.IsPlayerExist()) return Node.EStatus.FAILURE;

            var targetPosInRememberableRange = m_HumanoidDetection.m_RememberedLocationSensor.Scan();
            var bDoesRememberTargetPos = targetPosInRememberableRange != null;
            
            if (!bDoesRememberTargetPos) return Node.EStatus.FAILURE;
            
            m_Soldier.ChaseTarget(targetPosInRememberableRange.position);
            
            var bInAttackRange = m_HumanoidDetection.m_AttackRangeSensor.Scan() != null;
            return bInAttackRange ? Node.EStatus.SUCCESS : Node.EStatus.RUNNING;
        }

        private Node.EStatus StartAttack()
        {
            var targetInAttackRange = m_HumanoidDetection.m_AttackRangeSensor.Scan();
            var bIsTargetInAttackRange = targetInAttackRange != null;
            if (!bIsTargetInAttackRange) return Node.EStatus.FAILURE;
            m_Soldier.StartAttack(targetInAttackRange);
            return Node.EStatus.SUCCESS;
        }
        private Node.EStatus Attack()
        {
            if (!PlayerUtils.IsPlayerExist()) return Node.EStatus.SUCCESS;
            
            var targetInAttackRange = m_HumanoidDetection.m_AttackRangeSensor.Scan();
            var bIsTargetInAttackRange = targetInAttackRange != null;
            
            if (!bIsTargetInAttackRange) return Node.EStatus.SUCCESS;
            m_Soldier.Attack(targetInAttackRange);
            return Node.EStatus.RUNNING;
        }

        private Node.EStatus StopAttack()
        {
            m_Soldier.StopAttack();
            return Node.EStatus.SUCCESS;
        }
    }
}