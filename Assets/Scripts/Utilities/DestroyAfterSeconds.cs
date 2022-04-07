using UnityEngine;

namespace DenizYanar.Utilities
{
    public class DestroyAfterSeconds : MonoBehaviour
    {
        [SerializeField] private float _seconds;
        private void Start() => Destroy(gameObject, _seconds);
    }
}
