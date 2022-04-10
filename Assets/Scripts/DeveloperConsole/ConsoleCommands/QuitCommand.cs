using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    public class QuitCommand : ConsoleCommand
    {
        public override void Execute()
        {
            Application.Quit();
        }
    }
}
