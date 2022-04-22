using UnityEngine;

namespace DenizYanar.DamageAndHealthSystem
{
    public class Damage
    {
        public readonly float m_DamageValue;
        public readonly GameObject m_Author;
        

        public Damage(float damageValue, GameObject author)
        {
            m_DamageValue = damageValue;
            m_Author = author;
        }
    }
}
