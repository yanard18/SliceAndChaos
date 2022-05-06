using DenizYanar.DamageAndHealthSystem;
using DenizYanar.Guns;
using DenizYanar.Movement;
using DenizYanar.YanarPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    [RequireComponent(typeof(Rigidbody2D), typeof(IPathFind))]
    public class Soldier : Enemy
    {
        private SoldierBehaviour m_SoldierBehaviour;
        private IPathFind m_PathFind;
        private IMove m_iStopMovement;
        private IMove m_iHorizontalMovement;
        private Rigidbody2D m_Rb;


        
        [SerializeField] [Required]
        private Aim2D m_Aim2D;

        [SerializeField] [Required]
        private Gun m_Gun;


        protected override void Awake()
        {
            base.Awake();
            m_Rb = GetComponent<Rigidbody2D>();
            m_PathFind = GetComponent<IPathFind>();
            m_iHorizontalMovement = new HorizontalPhysicMovement(m_Rb, 5, 30);
            m_iStopMovement = new Stop(m_Rb);
        }

        protected override void EOnDeath(Damage damage)
        {
            Destroy(gameObject);
        }

        protected override void EOnTakeDamage(Damage damage)
        {
            throw new System.NotImplementedException();
        }

        public void ChaseTarget(Vector2 targetPos)
        {
            var dir = m_PathFind.CalculateDirection(targetPos);
            m_iHorizontalMovement.Move(dir);
        }


        public void StartAttack(Transform target)
        {
            m_iStopMovement.Move(Vector2.zero);
            m_Aim2D.StartToAim(target);
            m_Gun.StartFire();
        }

        public void Attack(Transform target)
        {
            var gunTransform = m_Gun.transform;
            var dirBetweenGunAndEnemy =
                YanarUtils.Direction(gunTransform.position, target.position);
            var angleBetweenGunAndEnemy = Vector2.Angle(gunTransform.right, dirBetweenGunAndEnemy);
            if (angleBetweenGunAndEnemy <= 15)
                m_Gun.StartFire();
            else
                m_Gun.StopFire();
        }


        public void StopAttack()
        {
            m_Gun.StopFire();
        }
    }
}