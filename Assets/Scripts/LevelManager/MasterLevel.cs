using UnityEngine;

namespace DenizYanar.LevelManagement
{
    [CreateAssetMenu(menuName = "Level Management/Main Level")]
    public class MasterLevel : Level
    {
        public LevelDependencyList DependencyList;
    }
}
