using DenizYanar.Core;
using UnityEngine;

namespace DenizYanar
{
    public class MeleeWeapon : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var target = other.transform.root.GetComponent<IDamage>();
            target?.TakeDamage();
        }
    }
}
