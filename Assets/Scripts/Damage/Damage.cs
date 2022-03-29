using UnityEngine;

namespace DenizYanar.Core
{
    public class Damage
    {
        public readonly float DamageValue;
        public readonly GameObject Author;
        

        public Damage(float damageValue, GameObject author)
        {
            DamageValue = damageValue;
            Author = author;
        }
    }
}
