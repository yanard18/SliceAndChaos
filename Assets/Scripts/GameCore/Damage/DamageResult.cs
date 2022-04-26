using UnityEngine;

namespace DenizYanar.DamageAndHealthSystem
{
    public class DamageResult
    {
        public readonly GameObject m_DamagedObject;
        public DamageResult(GameObject damagedObject)
        {
            m_DamagedObject = damagedObject;
        }
    }
}

