using DenizYanar.Core;
using DenizYanar.PlayerSystem;
using UnityEngine;

namespace DenizYanar
{
    public class DamagingHitBox : HitBox
    {
        [SerializeField] private bool _canDamageOnlyPlayer;
        [SerializeField] private float _damageValue;

        private void OnTriggerStay2D(Collider2D other)
        {
            var hitBox = other.GetComponent<HitBox>();
            if (hitBox != null) GiveDamage(hitBox); 
        }

        private void GiveDamage(HitBox hitBox)
        {
            if (_canDamageOnlyPlayer && !IsCollidedObjectPlayer(hitBox))
                return;


            var damage = new Damage(_damageValue, hitBox.Owner);
            hitBox.HealthOfHitBox.TakeDamage(damage);
        }
        

        
        
        
        
        

        private static bool IsCollidedObjectPlayer(HitBox hitBox)
        {
            return hitBox.Owner.transform == Player.s_Instance.transform;
        }
    }
}
 