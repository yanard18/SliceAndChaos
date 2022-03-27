using DenizYanar.Core;
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
            
            
            var target = other.transform.root.GetComponent<IDamageable>();
            target?.TakeDamage(new Damage(gameObject));
        }
    }
}
