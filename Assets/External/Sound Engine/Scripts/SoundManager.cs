using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DenizYanar.Audio
{
    public class SoundManager : MonoBehaviour
    {
        private ObjectPool<AudioEmitter> _pool;
        
        [SerializeField] private AudioCueEventChannel _cueEventChannel;
        [SerializeField] private AudioEmitter _audioEmitter;

      
        #region Monobehaviour

        private void OnEnable() => _cueEventChannel.PlayEvent += PlayAudioCue;

        private void OnDisable() => _cueEventChannel.PlayEvent -= PlayAudioCue;

        private void Start()
        {
            _pool = new ObjectPool<AudioEmitter>(
                () => Instantiate(_audioEmitter, _pool.RootObject),
                emitter => emitter.gameObject.SetActive(true),
                emitter => emitter.gameObject.SetActive(false),
                emitter => emitter.Init(_pool.Release)
            );
        }

        #endregion

        private void PlayAudioCue(AudioCue cue, Vector3 pos, AudioSettings overridenSettings)
        {
            switch (cue.AudioPlayType)
            {
                case EAudioPlayType.SINGLE:
                    SpawnAudioEmitter(cue.AudioClips[0], pos, cue, overridenSettings);
                    break;
                case EAudioPlayType.RANDOM:
                    var randomClipIndex = Random.Range(0, cue.AudioClips.Length);
                    var randomAudioClip = cue.AudioClips[randomClipIndex];
                    SpawnAudioEmitter(randomAudioClip, pos, cue, overridenSettings);
                    break;
                case EAudioPlayType.SEQUENCE:
                    StartCoroutine(PlayAudioSequence(cue.AudioClips, pos, cue, overridenSettings));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }



        private IEnumerator PlayAudioSequence(IEnumerable<AudioClip> clips, Vector3 pos, AudioCue cue, AudioSettings overridenSettings = null)
        {
            foreach (var clip in clips)
            {
                var duration = clip.length;
                SpawnAudioEmitter(clip, pos, cue, overridenSettings);
                yield return new WaitForSeconds(duration);
            }
        }
        
        
        
        
        private void SpawnAudioEmitter(AudioClip clip, Vector3 pos, AudioCue cue, AudioSettings overridenSettings = null)
        {
            var emitter = _pool.Get();
            var source = emitter.AudioSource;


            source.outputAudioMixerGroup = overridenSettings != null && overridenSettings.MixerGroup != null
                ? overridenSettings.MixerGroup
                : cue.Settings.MixerGroup;

            source.volume = overridenSettings?.Volume ?? cue.Settings.Volume;
            source.spatialBlend = overridenSettings?.SpatialBlend ?? cue.Settings.SpatialBlend;
            source.dopplerLevel = overridenSettings?.DopplerLevel ?? cue.Settings.DopplerLevel;
            source.panStereo = overridenSettings?.StereoPan ?? cue.Settings.StereoPan;
            
            

            emitter.transform.position = pos;
            emitter.Play(clip);

        }
    }  
}

