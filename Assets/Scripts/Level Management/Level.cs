using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.LevelManagement
{
    public class Level : ScriptableObject
    {

        [ValidateInput("@$value.Length > 0", "Level name is required.")]
        public string m_LevelName;
        
        [TextArea]
        public string m_Description;
        
        [TextArea]
        public string m_LoadDescription;
    }
}