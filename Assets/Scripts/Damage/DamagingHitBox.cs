using DenizYanar.DamageAndHealthSystem;
using DenizYanar.PlayerSystem;
using UnityEngine;

namespace DenizYanar
{
    public class DamagingHitBox : HitBox
    {
        [SerializeField] private bool m_bDamageOnlyPlayer;
        [SerializeField] private float m_DamageValue;

        private void OnTriggerStay2D(Collider2D other)
        {
            var hitBox = other.GetComponent<HitBox>();
            if (hitBox != null) GiveDamage(hitBox); 
        }

        private void GiveDamage(HitBox hitBox)
        {
            if (m_bDamageOnlyPlayer && !IsCollidedObjectPlayer(hitBox))
                return;


            var damage = new Damage(m_DamageValue, hitBox.m_Owner);
            hitBox.m_HealthOfHitBox.TakeDamage(damage);
        }
              

        private static bool IsCollidedObjectPlayer(HitBox hitBox) => hitBox.m_Owner.transform == Player.s_Instance.transform;
    }
}
 