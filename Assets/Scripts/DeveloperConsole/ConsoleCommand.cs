using System;
using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    public abstract class ConsoleCommand : ScriptableObject
    {
        public string m_CommandName;

        [TextArea] 
        public string m_Usage;

        public int m_nParameter;

        public abstract void Execute(string[] parameters);


        public bool IsCommandValid(string[] parameters)
        {
            return IsParametersEqual(parameters);
        }
        
        private bool IsParametersEqual(string[] parameters)
        {
            if (parameters.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(parameters));
            return parameters.Length == m_nParameter + 1;
        }
    }
}
