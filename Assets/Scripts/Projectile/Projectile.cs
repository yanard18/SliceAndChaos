using System;
using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rb;

        public GameObject Author { get; private set; }
        

        [SerializeField] private LayerMask _obstacleLayer;

        public event Action OnHit;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Init(Vector2 trajectory, float angularVelocity = 0, GameObject author = null)
        {
            Author = author != null ? author : null;
            _rb.velocity = trajectory;
            _rb.angularVelocity = angularVelocity;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject == Author || other.transform.root.gameObject == Author)
                return;
            
            Debug.Log(other.gameObject.name);
            
            OnHit?.Invoke();
        }

        public void Stop()
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }
}
