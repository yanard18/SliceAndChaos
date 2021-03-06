using DenizYanar.SenseEngine;
using UnityEngine;

namespace DenizYanar.SenseEngine
{
	[SenseEnginePath("GameObjects/Destroy Object")]
    public class SenseDestroyObject : Sense
    {
        [Header("Destroy Settings")]
        
        public GameObject TargetObject;
        
		public float DurationToDestroy;

		private void Awake()
		{
			Label = "Destroy Object";
		}

		public override void Play()
		{
			var objectToDestroy = TargetObject != null 
				? TargetObject 
				: transform.root.gameObject;



			if (DurationToDestroy <= 0)
				Destroy(objectToDestroy);
			else
				Destroy(objectToDestroy, DurationToDestroy);
		}
	}
}
