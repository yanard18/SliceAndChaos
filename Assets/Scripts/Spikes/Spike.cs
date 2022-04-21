using DenizYanar.DamageAndHealthSystem;
using UnityEngine;

namespace DenizYanar
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private bool _effectOnlyPlayer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (_effectOnlyPlayer)
                if(!other.transform.root.CompareTag("Player")) return;
            
            
            var target = other.transform.root.GetComponent<Health>();
            target?.TakeDamage(new Damage(9999.9f, gameObject));
        }
    }
}
