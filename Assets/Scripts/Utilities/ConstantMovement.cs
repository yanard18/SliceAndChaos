using UnityEngine;


namespace DenizYanar.Utilities
{
    public class ConstantMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction;

        private void Update() => transform.Translate(_direction * Time.deltaTime);
    }
}
