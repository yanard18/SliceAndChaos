using System;
using UnityEngine;


namespace DenizYanar.Audio
{
    public class AudioEmitter : MonoBehaviour
    {
        public AudioSource AudioSource;

        private Action<AudioEmitter> _releaseAction;
        

        private void Awake() => AudioSource = GetComponent<AudioSource>();
        
        
        
        public void Init(Action<AudioEmitter> destroy) =>  _releaseAction ??= destroy;
        

        public void Play(AudioClip audioClip)
        {
            
            
            var audioDuration = audioClip.length;
            AudioSource.PlayOneShot(audioClip);
            Invoke(nameof(Release), audioDuration);
        }

        private void Release() => _releaseAction(this);
    }

}


