using UnityEngine;

namespace DenizYanar.Core
{
    public class Damage
    {
        public readonly GameObject Author;

        public Damage()
        {
            
        }

        public Damage(GameObject author)
        {
            Author = author;
        }
    }
}
