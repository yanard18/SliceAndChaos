using UnityEngine;

namespace DenizYanar.Audio
{
    [CreateAssetMenu(menuName = "Sound Engine/Audio Cue")]
    public class AudioCue : ScriptableObject
    {
        
        [SerializeField] private AudioClip[] _audioClips;
        public AudioClip[] AudioClips => _audioClips;
        

        public AudioSettings Settings;
        
        [SerializeField] private EAudioPlayType _audioPlayType;
        public EAudioPlayType AudioPlayType => _audioPlayType;
    }

    public enum EAudioPlayType
    {
        SINGLE,
        RANDOM,
        SEQUENCE
    }


}
