using System.Collections.Generic;
using DenizYanar.Events;
using TMPro;
using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    public class DeveloperConsole : MonoBehaviour
    {
        private readonly Dictionary<string, ConsoleCommand> m_TCommandDictionary = new();
        private bool m_bConsoleOpen;


        [Header("Developer Panel UI")] 
        [SerializeField] private GameObject m_Console;
        [SerializeField] private TMP_Text m_ConsoleLog;
        [SerializeField] private TMP_InputField m_InputField;
        

        [Header("Configurations")]
        [SerializeField] private char m_CommandPrefix = '/';
        [SerializeField] private int m_nMaxLineInConsole = 30;
        [SerializeField] private ConsoleCommandTable m_CommandTable;
        [SerializeField] private PlayerInputs m_Inputs;
        [SerializeField] private StringEventChannelSO m_ecChangeInputActionMap;


        private void Awake()
        {
            AddCommandsToDictionary();
        }

        private void AddCommandsToDictionary()
        {
            foreach (var command in m_CommandTable.m_Commands)
            {
                if (m_TCommandDictionary.ContainsKey(command.m_CommandName))
                    continue;

                m_TCommandDictionary.Add(command.m_CommandName, command);
            }
        }

        private void OnEnable()
        {
            m_Inputs.OnOpenDevConsoleKeyPressed += ToggleConsole;
            m_Inputs.OnCloseDevConsoleKeyPressed += ToggleConsole;
            m_Inputs.OnEnterCommandKeyPressed += ApplyInputField;
        }

        private void OnDisable()
        {
            m_Inputs.OnOpenDevConsoleKeyPressed -= ToggleConsole;
            m_Inputs.OnCloseDevConsoleKeyPressed -= ToggleConsole;
            m_Inputs.OnEnterCommandKeyPressed -= ApplyInputField;
        }

        private void ToggleConsole()
        {
            if (m_bConsoleOpen)
                CloseConsole();
            else
                OpenConsole();
        }

        private void CloseConsole()
        {
            m_bConsoleOpen = false;
            m_Console.SetActive(false);
            m_ecChangeInputActionMap.Invoke("Player");
            
        }

        private void OpenConsole()
        {
            m_bConsoleOpen = true;
            m_Console.SetActive(true);
            m_InputField.ActivateInputField();
            m_ecChangeInputActionMap.Invoke("Console");
        }

        private void ApplyInputField()
        {
            ProcessInput(m_InputField.text);
            ClearInputField();
            m_InputField.ActivateInputField();
        }

        private void SendMessageToConsole(string message)
        {
            m_ConsoleLog.text = ClampLines(m_ConsoleLog.text, m_nMaxLineInConsole);
            m_ConsoleLog.text += message + '\n';
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
            var command = Interpreter.NameToCommand(parsedInput[0], m_TCommandDictionary);
            if (command == null) return;


            if(command.IsCommandValid(parsedInput))
                command.Execute(parsedInput);
            else
                SendMessageToConsole(command.m_Usage);
        }

        private static bool HasNoInput(string input) => input == string.Empty;

        private static string RemovePrefix(string input) => input.Remove(0, 1);

        private void ClearInputField() => m_InputField.text = string.Empty;

        private bool HasCommandPrefix(string input) => input[0] == m_CommandPrefix;
    }
}
