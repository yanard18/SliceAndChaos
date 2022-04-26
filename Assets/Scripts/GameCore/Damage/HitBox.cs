using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.DamageAndHealthSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class HitBox : MonoBehaviour, IDamage
    {
        [Required]
        public Health m_HealthOfHitBox;

        public void TakeDamage(Damage damage) => m_HealthOfHitBox.TakeDamage(damage);
    }
}
