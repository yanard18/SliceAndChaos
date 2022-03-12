using DenizYanar.Core;
using UnityEngine;

namespace DenizYanar
{
    public class Box : MonoBehaviour, IDamageable
    {
        public void TakeDamage(Damage damage)
        {
            Destroy(gameObject);
        }
    }
}
