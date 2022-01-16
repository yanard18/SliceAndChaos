using System;
using DenizYanar;
using UnityEngine;

namespace DenizYanar.Audio
{
    public class AudioTester : MonoBehaviour
    {
        [SerializeField] private AudioCueEventChannel _cueEventChannel;

        [SerializeField] private AudioCue _audioCue, _audioCue2;

        
        
        private void Start()
        {

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                _cueEventChannel.Invoke(_audioCue);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                var settings = new AudioSettings(volume: 0.5f, stereoPan: 1.0f);
                _cueEventChannel.Invoke(_audioCue2, pos: Vector3.one * 999, overridenSettings: settings);
            }
            

        }
    }
}


