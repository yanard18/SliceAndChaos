using System;
using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rb;

        [SerializeField] private LayerMask _hitBoxLayer;

        public GameObject Author { get; private set; }

        public event Action<Collider2D> OnHit;
        

        #region Monobehaviour

            private void Awake() => _rb = GetComponent<Rigidbody2D>();

            private void FixedUpdate()
            {
                DetectHit();
            }

            private void DetectHit()
            {
                var velocity = _rb.velocity;
                var currentPosition = _rb.position;
                var desiredVelocityVector = velocity * Time.fixedDeltaTime;
                var hit = Physics2D.CircleCast(
                    currentPosition,
                    0.12f,
                    desiredVelocityVector.normalized,
                    desiredVelocityVector.magnitude,
                    _hitBoxLayer);

                if (!hit) return;

                OnHit?.Invoke(hit.collider);
            }

            #endregion

        public void Init(Vector2 trajectory, float angularVelocity = 0, float lifeTime = 5.0f, GameObject author = null)
        {
            Author = author != null ? author : null;
            _rb.velocity = trajectory;
            _rb.angularVelocity = angularVelocity;
            if(lifeTime > 0f)
                Destroy(gameObject, lifeTime);
        }
        
        public void Stop()
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }
}
