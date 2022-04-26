using DenizYanar.Audio;
using UnityEngine;

namespace DenizYanar.DeveloperConsoleSystem
{
    [CreateAssetMenu(menuName = "Developer Console/Commands/Meow")]
    public class MeowCommand : ConsoleCommand
    {
        [SerializeField]
        private AudioCue m_Audio;
        
        [SerializeField]
        private AudioCueEventChannel m_EventChannel;
        
        public override void Execute(string[] parameters)
        {
            m_EventChannel.Invoke(m_Audio);
        }
    }
}
