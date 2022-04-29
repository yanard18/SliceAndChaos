using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.SenseEngine
{
    [CreateAssetMenu(menuName = "Sense Engine/Cinemachine Camera Shake Event Channel")]
    public class CinemachineImpulseSenseEvent : ScriptableObject
    {
        public UnityAction<CinemachineImpulseDefinition> ImpulseEvent;

        public void Invoke(CinemachineImpulseDefinition impulseDefinition)
        {
            ImpulseEvent?.Invoke(impulseDefinition);
        }
    }
}
