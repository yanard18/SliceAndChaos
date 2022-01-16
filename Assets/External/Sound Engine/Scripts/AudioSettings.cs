using System;
using UnityEngine.Audio;

namespace DenizYanar.Audio
{
    [Serializable]
    public class AudioSettings
    {
        public AudioMixerGroup MixerGroup;
        public float Volume;
        public float SpatialBlend;
        public float DopplerLevel;
        public float StereoPan;
        
        
        
        public AudioSettings
        (
            AudioMixerGroup mixerGroup = null,
            float volume = 1.0f,
            float spatialBlend = 1.0f,
            float dopplerLevel = 1.0f,
            float stereoPan = 0
            )
        
        {
            Volume = volume;
            SpatialBlend = spatialBlend;
            MixerGroup = mixerGroup;
            DopplerLevel = dopplerLevel;
            StereoPan = stereoPan;
        }
    } 
}
