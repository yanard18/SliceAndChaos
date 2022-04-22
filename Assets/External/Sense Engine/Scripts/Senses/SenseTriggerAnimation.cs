using DenizYanar.SenseEngine;
using UnityEngine;


namespace DenizYanar.SenseEngine
{
    [SenseEnginePath("Animations/Trigger Animation")]
    public class SenseTriggerAnimation : Sense
    {
        [SerializeField] private string _triggerName;
        [SerializeField] private Animator _animator;
        
        private void Awake()
        {
            Label = "Trigger Animation";
        }

        public override void Play()
        {  
            _animator.SetTrigger(_triggerName);
        }
    }
}
