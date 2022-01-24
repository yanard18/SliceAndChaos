using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TelekinesisObject : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _currentVelocity;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        

        public void Force(Vector2 targetPos, float smoothTime)
        {
            var desiredPos = Vector2.SmoothDamp(transform.position, targetPos, ref _currentVelocity, smoothTime);
            _rb.AddTorque(Time.fixedDeltaTime * 25f);
            _rb.MovePosition(desiredPos);
        }

        public void OnRelease()
        {
            _rb.velocity = _currentVelocity * 2;
            _rb.AddTorque(360.0f);
        }
    }
    
}
