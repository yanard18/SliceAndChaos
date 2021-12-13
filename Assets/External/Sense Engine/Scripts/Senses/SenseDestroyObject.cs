using UnityEngine;

namespace DenizYanar.SenseEngine
{
	[SenseEnginePath("GameObjects/Destroy Object")]
    public class SenseDestroyObject : Sense
    {
        [Header("Destroy Settings")]

        [SerializeField]
        private GameObject TargetObject;

		[SerializeField]
		private float DurationToDestroy = 0.0f;

		private void Awake()
		{
			Label = "Destroy Object";
		}

		public override void Play()
		{
			GameObject objectToDestroy = TargetObject != null 
				? TargetObject 
				: transform.root.gameObject;



			if (DurationToDestroy <= 0)
				Destroy(objectToDestroy);
			else
				Destroy(objectToDestroy, DurationToDestroy);
		}
	}
}
