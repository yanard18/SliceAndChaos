using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.LevelManagement
{
    [CreateAssetMenu(menuName = "Level Management/Main Level")]
    public class MasterLevel : Level
    {
        [Required]
        public LevelDependencyList m_DependencyList;
    }
}
