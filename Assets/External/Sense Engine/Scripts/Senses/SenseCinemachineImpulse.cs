using UnityEngine;
using Cinemachine;
using DenizYanar.SenseEngine;

namespace DenizYanar.SenseEngine
{
    [SenseEnginePath("Camera/Cinemachine Impulse")]
    public class SenseCinemachineImpulse : Sense
    {
        [SerializeField] private CinemachineImpulseSenseEvent _impulseEventChannel;
        [SerializeField] private CinemachineImpulseDefinition _impulseDefinition;

        private void Awake() => Label = "Cinemachine Impulse";

        public override void Play()
        {
            if (Camera.main is null) return;
            
            _impulseEventChannel.Invoke(_impulseDefinition);
        }
    }
}
