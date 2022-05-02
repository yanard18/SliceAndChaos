using System.Collections.Generic;
using DenizYanar.DamageAndHealthSystem;

namespace DenizYanar.Attacks
{
    public interface IAttack
    {
        List<DamageResult> Attack();
    }
}