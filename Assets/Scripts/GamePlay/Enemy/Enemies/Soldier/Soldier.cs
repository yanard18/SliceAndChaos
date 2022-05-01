using System;
using Cinemachine;
using DenizYanar.DamageAndHealthSystem;
using DenizYanar.Movement;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    public class Soldier : Enemy
    {
        private SoldierBehaviour m_SoldierBehaviour;
        private IMove m_HorizontalMovement;
        private IPathFind m_PathFind;
        private Rigidbody2D m_Rb;
        
        
        protected override void Awake()
        {
            base.Awake();
            m_Rb = GetComponent<Rigidbody2D>();
            m_PathFind = GetComponent<IPathFind>();
            m_HorizontalMovement = new HorizontalPhysicMovement(m_Rb, 10, 10);
        }

        protected override void EOnDeath(Damage damage)
        {
            throw new System.NotImplementedException();
        }

        protected override void EOnTakeDamage(Damage damage)
        {
            throw new System.NotImplementedException();
        }

        public void ChaseTarget(Vector2 targetPos)
        {
            Debug.Log("Chase");
            var dir = m_PathFind.CalculateDirection(targetPos);
            m_HorizontalMovement.Move(dir);
        }
        public void Attack()
        {
            Debug.Log("Started To Attack");
        }
    }
}