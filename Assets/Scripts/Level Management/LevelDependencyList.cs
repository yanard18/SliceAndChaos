using UnityEngine;

namespace DenizYanar.LevelManagement
{
    [CreateAssetMenu (menuName = "Level Management/Level Dependency List")]
    public class LevelDependencyList : ScriptableObject
    {
        public DependencyLevel[] LevelList;
    }
}
