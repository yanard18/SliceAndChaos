using UnityEngine;
using DenizYanar.SenseEngine;

namespace DenizYanar
{
    public class CubeController : MonoBehaviour
    {
		[SerializeField]
		private SenseEnginePlayer _engine;

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				_engine.Play();
			}
		}
	}
}
