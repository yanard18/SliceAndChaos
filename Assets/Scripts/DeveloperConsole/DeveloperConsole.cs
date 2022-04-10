using TMPro;
using UnityEngine;


namespace DenizYanar.DeveloperConsoleSystem
{
    public class DeveloperConsole : MonoBehaviour
    {
        [Header("Developer Panel UI")] 
        [SerializeField] private GameObject _console;
        [SerializeField] private TMP_Text _consoleLog;
        [SerializeField] private TMP_InputField _inputField;
        
        
        private bool _isConsoleActive;
        
        [Header("Configurations")]
        [SerializeField] private ConsoleCommandTable _commandTable;
        [SerializeField] private char _commandPrefix = '/';
        [SerializeField] private PlayerInputs _inputs;


        private void OnEnable()
        {
            _inputs.OnDevConsoleKeyPressed += ToggleConsole;
        }

        private void OnDisable()
        {
            _inputs.OnDevConsoleKeyPressed -= ToggleConsole;
        }

        private void ToggleConsole()
        {
            if (_isConsoleActive)
            {
                _isConsoleActive = false;
                _console.SetActive(false);
            }
            else
            {
                _isConsoleActive = true;
                _console.SetActive(true);
            }
        }

        private void SendMessageToConsole(string message)
        {
            _consoleLog.text += message + '\n';
        }
        
        public void ProcessInput(string input)
        {
            SendMessageToConsole(input);
            ClearInputField();
            return;
            if(!HasCommandPrefix(input)) return;
            var parsedInput = Interpreter.ParseTheCommand(input);
            var command = Interpreter.NameToCommand(parsedInput[0], _commandTable);
            
            if(command == null) return;
            
            command.Execute();
        }

        private void ClearInputField() => _inputField.text = string.Empty;

        private bool HasCommandPrefix(string input) => input[0] == _commandPrefix;
    }
}
