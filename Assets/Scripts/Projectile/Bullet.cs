using UnityEngine;

namespace DenizYanar.Projectiles
{
    public class Bullet : Projectile
    {
        protected override void Hit(Collider2D col)
        {
            if(col.transform.root.CompareTag("Player"))
                Debug.Log("Player");
        }
    }
}
