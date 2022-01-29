using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TelekinesisObject : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _currentVelocity;

        [SerializeField] private float _maxDistanceDifference = 15.0f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }


        public void Force(Vector2 targetPos, float smoothTime)
        {
            var position = transform.position;
            var dir = targetPos - (Vector2) position;
            dir = Vector2.ClampMagnitude(dir, _maxDistanceDifference);
            var modifiedTarget = dir + (Vector2) position;
            var desiredPos = Vector2.SmoothDamp(position, modifiedTarget, ref _currentVelocity, smoothTime);
            _rb.AddTorque(Time.fixedDeltaTime * 25f);
            _rb.MovePosition(desiredPos);
        }

        public void OnRelease()
        {
            if(_currentVelocity == Vector2.zero) return;
            
            _rb.velocity = _currentVelocity * 2;
            _currentVelocity = Vector2.zero;
            _rb.AddTorque(360.0f);
        }
    }
    
}
