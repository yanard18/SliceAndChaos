using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    public abstract class ConsoleCommand : ScriptableObject
    {
        public string CommandName;
        public abstract void Execute(); 
    }
}
