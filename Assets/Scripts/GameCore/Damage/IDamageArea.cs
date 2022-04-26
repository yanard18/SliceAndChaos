using System.Collections.Generic;

namespace DenizYanar.DamageAndHealthSystem
{
    public interface IDamageArea
    {
        List<DamageResult> CreateArea(Damage damage);
    }

    
}