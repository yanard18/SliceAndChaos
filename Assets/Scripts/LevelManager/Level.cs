using UnityEngine;

namespace DenizYanar.LevelManagement
{
    [CreateAssetMenu(menuName = "Level Management/Level")]
    public class Level : ScriptableObject
    {
        public string LevelName;
        public LevelDependencies Dependencies;

        [TextArea] public string Description;
        [TextArea] public string LoadDescription;
    }
}