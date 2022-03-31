using UnityEngine;

namespace DenizYanar.LevelManagement
{
    [CreateAssetMenu (menuName = "Level Management/Level Dependencies")]
    public class LevelDependencies : ScriptableObject
    {
        public Level[] LevelList;
    }
}
