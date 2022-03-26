using DenizYanar.Core;
using UnityEngine;
using DenizYanar.Player;

namespace DenizYanar.Projectiles
{
    public class Bullet : Projectile
    {
        protected override void Hit(Collider2D col)
        {
            var target = col.GetComponentInParent<IDamageable>();
            
            if(target is null) return;

            var damage = new Damage(Author);
            target.TakeDamage(damage);
            
        }
    }
}
