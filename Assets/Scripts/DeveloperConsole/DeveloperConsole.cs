using System.Collections.Generic;
using System.Net.Mime;
using DenizYanar.Events;
using TMPro;
using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    public class DeveloperConsole : MonoBehaviour
    {
        private readonly Dictionary<string, ConsoleCommand> _commandDictionary = new();
        private bool _isConsoleOpen;


        [Header("Developer Panel UI")] 
        [SerializeField] private GameObject _console;
        [SerializeField] private TMP_Text _consoleLog;
        [SerializeField] private TMP_InputField _inputField;
        

        [Header("Configurations")]
        [SerializeField] private ConsoleCommandTable _commandTable;
        [SerializeField] private PlayerInputs _inputs;
        [SerializeField] private char _commandPrefix = '/';
        [SerializeField] private StringEventChannelSO _onInputMapChange;
        [SerializeField] private int _maxLineCountInConsole = 30;


        private void Awake()
        {
            AddCommandsToDictionary();
        }

        private void AddCommandsToDictionary()
        {
            foreach (var command in _commandTable.Commands)
            {
                if (_commandDictionary.ContainsKey(command.CommandName))
                    continue;

                _commandDictionary.Add(command.CommandName, command);
            }
        }

        private void OnEnable()
        {
            _inputs.OnOpenDevConsoleKeyPressed += ToggleConsole;
            _inputs.OnCloseDevConsoleKeyPressed += ToggleConsole;
            _inputs.OnEnterCommandKeyPressed += ApplyInputField;
        }

        private void OnDisable()
        {
            _inputs.OnOpenDevConsoleKeyPressed -= ToggleConsole;
            _inputs.OnCloseDevConsoleKeyPressed -= ToggleConsole;
            _inputs.OnEnterCommandKeyPressed -= ApplyInputField;
        }

        private void ToggleConsole()
        {
            if (_isConsoleOpen)
                CloseConsole();
            else
                OpenConsole();
        }

        private void CloseConsole()
        {
            _isConsoleOpen = false;
            _console.SetActive(false);
            _onInputMapChange.Invoke("Player");
            
        }

        private void OpenConsole()
        {
            _isConsoleOpen = true;
            _console.SetActive(true);
            _inputField.ActivateInputField();
            _onInputMapChange.Invoke("Console");
        }

        private void ApplyInputField()
        {
            ProcessInput(_inputField.text);
            ClearInputField();
            _inputField.ActivateInputField();
        }

        private void SendMessageToConsole(string message)
        {
            _consoleLog.text = ClampLines(_consoleLog.text, _maxLineCountInConsole);
            _consoleLog.text += message + '\n';
        }

        private string ClampLines(string text, int maxLineLength)
        {
            var lines = text.Split('\n');
            return lines.Length > maxLineLength ? text.Remove(0, lines[0].Length + 1) : text;
        }
        
        private void ProcessInput(string input)
        {
            if(HasNoInput(input)) return;
            SendMessageToConsole(input);
            
            if(HasCommandPrefix(input))
                RunCommand(input);
        }

        private void RunCommand(string input)
        {
            input = RemovePrefix(input);

            var parsedInput = Interpreter.ParseTheCommand(input);
            var command = Interpreter.NameToCommand(parsedInput[0], _commandDictionary);
            if (command == null) return;


            if(command.IsCommandValid(parsedInput))
                command.Execute(parsedInput);
            else
                SendMessageToConsole(command.Usage);
        }

        private static bool HasNoInput(string input) => input == string.Empty;

        private static string RemovePrefix(string input) => input.Remove(0, 1);

        private void ClearInputField() => _inputField.text = string.Empty;

        private bool HasCommandPrefix(string input) => input[0] == _commandPrefix;
    }
}
