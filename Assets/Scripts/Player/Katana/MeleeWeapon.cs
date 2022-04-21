using DenizYanar.DamageAndHealthSystem;
using UnityEngine;

namespace DenizYanar
{
    public class MeleeWeapon : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.transform.root == transform.root) return;
            
            var target = other.transform.root.GetComponent<Health>();
            var player = transform.root.gameObject;
            var damage = new Damage(10, player);

            target?.TakeDamage(damage);

        }
    }
}
