using DenizYanar.Core;
using UnityEngine;

namespace DenizYanar
{
    public class Box : MonoBehaviour, IDamage
    {
        public void TakeDamage()
        {
            Destroy(gameObject);
        }
    }
}
