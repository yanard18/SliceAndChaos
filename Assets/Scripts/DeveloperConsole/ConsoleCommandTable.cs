using System.Collections.Generic;
using DenizYanar.DeveloperConsoleSystem;
using UnityEngine;

namespace DenizYanar
{
    public class ConsoleCommandTable : MonoBehaviour
    {
        public readonly Dictionary<string, ConsoleCommand> Commands = new Dictionary<string, ConsoleCommand>();

        public void Register(ConsoleCommand c) => Commands.Add(c.CommandName, c);

        public void Unregister(ConsoleCommand c) => Commands.Remove(c.CommandName);
    }
}
