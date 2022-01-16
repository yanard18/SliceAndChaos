using System;
using UnityEngine;

namespace DenizYanar.Audio
{
    [CreateAssetMenu (menuName = "Sound Engine/Audio Cue Event Channel")]
    public class AudioCueEventChannel : ScriptableObject
    {
        public event Action<AudioCue, Vector3, AudioSettings> PlayEvent;

        public void Invoke(AudioCue audioCue, Vector3? pos = null, AudioSettings overridenSettings = null)
        {
            pos ??= Vector3.zero;
            PlayEvent?.Invoke(audioCue, pos.Value, overridenSettings);
        }
    } 
}

