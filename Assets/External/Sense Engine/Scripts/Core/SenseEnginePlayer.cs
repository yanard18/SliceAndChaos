using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.SenseEngine
{
    public class SenseEnginePlayer : MonoBehaviour
    {
		[SerializeField]
        public List<Sense> SenseList = new List<Sense>();

		public void Play()
		{
			foreach (Sense s in SenseList)
				s.Play();
		}
	}
}
