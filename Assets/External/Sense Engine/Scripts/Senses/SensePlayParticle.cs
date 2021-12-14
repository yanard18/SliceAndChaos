using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar.External.Sense_Engine.Scripts.Senses
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
