using UnityEngine;

namespace DenizYanar.LevelManagement
{
    public class Level : ScriptableObject
    {
        public string LevelName;
        
        [TextArea] public string Description;
        [TextArea] public string LoadDescription;

        
    }
}