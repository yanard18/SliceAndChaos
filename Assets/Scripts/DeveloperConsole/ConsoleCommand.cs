using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    public abstract class ConsoleCommand : ScriptableObject
    {
        public string CommandName;

        [TextArea] 
        public string Usage;

        public int ParameterCount;

        public abstract void Execute(string[] parameters);


        public bool IsCommandValid(string[] parameters)
        {
            return IsParametersEqual(parameters);
        }
        
        private bool IsParametersEqual(string[] parameters) => parameters.Length == ParameterCount + 1;
    }
}
