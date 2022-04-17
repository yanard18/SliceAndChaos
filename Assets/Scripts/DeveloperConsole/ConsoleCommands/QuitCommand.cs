using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    [CreateAssetMenu(menuName="Developer Console/Commands/Quit")]
    public class QuitCommand : ConsoleCommand
    {
        public override void Execute(string[] input)
        {
            Application.Quit();
        }
    }
}
