using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    [CreateAssetMenu(menuName="Developer Console/Commands/Application Commands")]
    public class QuitCommand : ConsoleCommand
    {
        public override void Execute()
        {
            Application.Quit();
        }
    }
}
