using DenizYanar.Audio;
using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    [CreateAssetMenu(menuName = "Developer Console/Commands/Meow")]
    public class MeowCommand : ConsoleCommand
    {
        [SerializeField] private AudioCue _effect;
        [SerializeField] private AudioCueEventChannel _eventChannel;
        
        public override void Execute(string[] parameters)
        {
            _eventChannel.Invoke(_effect);
        }
    }
}
