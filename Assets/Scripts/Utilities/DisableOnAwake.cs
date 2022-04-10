using UnityEngine;

namespace DenizYanar.Utilities
{
    public class DisableOnAwake : MonoBehaviour
    {
        private void Awake() => gameObject.SetActive(false);
    }
}
