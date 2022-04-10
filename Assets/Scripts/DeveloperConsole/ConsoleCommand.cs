using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    public abstract class ConsoleCommand : ScriptableObject
    {
        [SerializeField] private ConsoleCommandTable _commandList;
        public string CommandName;

        private void OnEnable()
        {
            _commandList.Register(this);
        }

        private void OnDisable()
        {
            _commandList.Unregister(this);
        }


        public abstract void Execute(); 
    }
}
