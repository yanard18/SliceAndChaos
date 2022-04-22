using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.LevelManagement
{
    [CreateAssetMenu (menuName = "Level Management/Level Dependency List")]
    public class LevelDependencyList : ScriptableObject
    {
        [Required]
        public DependencyLevel[] m_LevelList;
    }
}
