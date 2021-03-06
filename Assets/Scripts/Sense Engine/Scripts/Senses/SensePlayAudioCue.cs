using UnityEngine;
using DenizYanar.Audio;
using DenizYanar.SenseEngine;
using AudioSettings = DenizYanar.Audio.AudioSettings;

namespace DenizYanar.SenseEngine
{
    [SenseEnginePath("Audio/Play Audio Cue")]
    public class SensePlayAudioCue : Sense
    {
        [SerializeField] private AudioCue _audioCue;
        [SerializeField] private AudioCueEventChannel _audioChannel;

        [SerializeField] private bool _useOverridenSettings;
        [SerializeField] private AudioSettings _overridenSettings;

        private void Awake()
        {
            Label = "Audio Cue";
        }

        public override void Play()
        {
            if(!_useOverridenSettings)
                _audioChannel.Invoke(_audioCue);
            else
                _audioChannel.Invoke(_audioCue, overridenSettings: _overridenSettings);
        }
    }
}
