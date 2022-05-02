using System.Collections.Generic;
using DenizYanar.DamageAndHealthSystem;
using UnityEngine;

namespace DenizYanar.Attacks
{
    public class SwordSlashAttack : IAttack
    {
        private readonly float m_DamageValue;
        private readonly IDamageArea m_DamageArea;
        private readonly Transform m_OwnerOfAttack;
        
        
        public SwordSlashAttack(float damageValue, IDamageArea damageArea, Transform ownerOfAttack)
        {
            m_DamageValue = damageValue;
            m_DamageArea = damageArea;
            m_OwnerOfAttack = ownerOfAttack;
        }

        public List<DamageResult> Attack()
        {
            Vector2 playerPosition = m_OwnerOfAttack.position;

            var damage = new Damage(
                m_DamageValue,
                m_OwnerOfAttack.gameObject
            );

            return m_DamageArea.CreateArea(damage);
        }
    }
}