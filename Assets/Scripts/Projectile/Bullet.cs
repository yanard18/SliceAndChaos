using DenizYanar.Core;
using UnityEngine;
using DenizYanar.PlayerSystem;

namespace DenizYanar.Projectiles
{
    public class Bullet : Projectile
    {
        protected override void Hit(Collider2D col)
        {
            var target = col.GetComponentInParent<Health>();
            
            if(target is null) return;

            var damage = new Damage(10, Author);
            target.TakeDamage(damage);
            
        }
    }
}
