using DenizYanar.DeveloperConsoleSystem;
using UnityEngine;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Developer Console/Commands Table")]
    public class ConsoleCommandTable : ScriptableObject
    {
        public ConsoleCommand[] Commands;
        
    }
}
