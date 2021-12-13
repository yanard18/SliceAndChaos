using UnityEngine;

namespace DenizYanar.SenseEngine
{
	[SenseEnginePath("Particle/Play Particle")]
	public class SensePlayParticle : Sense
	{
		[SerializeField]
		private ParticleSystem _particle;

		private void Awake()
		{
			Label = "Play Particle";
		}

		public override void Play()
		{
			_particle.Play();
		}
	}
}
