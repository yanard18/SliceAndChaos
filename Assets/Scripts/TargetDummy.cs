using DenizYanar.Core;

namespace DenizYanar
{
    public class TargetDummy : Entity, IDamage
    {
        public void TakeDamage()
        {
            Destroy(gameObject);
        }
    }
}
