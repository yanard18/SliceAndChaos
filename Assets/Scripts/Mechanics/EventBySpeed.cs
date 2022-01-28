using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar.Core
{
    public class EventBySpeed : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float _speedThreshold;
        [SerializeField] private UnityEvent _event;

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.relativeVelocity.magnitude > _speedThreshold)
                _event?.Invoke();
        }
    }
}
