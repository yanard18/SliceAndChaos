using System;
using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private bool _hit = false;

        [SerializeField] private LayerMask _hitBoxLayer;
        
        [SerializeField] private bool _enableDebug;

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
                if(_hit) return;
                
                var velocity = _rb.velocity;
                var currentPosition = _rb.position;
                var desiredVelocityVector = velocity * Time.fixedDeltaTime;
                RaycastHit2D[] hit = Physics2D.CircleCastAll(
                    currentPosition,
                    0.4f,
                    desiredVelocityVector.normalized,
                    desiredVelocityVector.magnitude,
                    _hitBoxLayer);
                
                
                #if UNITY_EDITOR
                if(_enableDebug)
                    Debug.DrawRay(currentPosition, desiredVelocityVector, Color.magenta, 5.0f);
                #endif
               
                

                if(hit.Length <= 0) return;

                foreach (var t in hit)
                {
                    #if UNITY_EDITOR
                    if(_enableDebug)
                        Debug.Log(t.transform.name);
                    #endif
                    
                    
                    
                    if (t.transform.gameObject == Author) continue;

                    _hit = true;
                    transform.position = t.point;
                    OnHit?.Invoke(t.collider);
                    
                    
                    
                    
                    #if UNITY_EDITOR
                    if(_enableDebug)
                        Debug.Log("Hit to: " + t.transform.name);
                    #endif
                    
                    
                    
                    return;
                }
            }

            #endregion

        public void Init(Vector2 trajectory, float angularVelocity = 0, float lifeTime = 5.0f, GameObject author = null)
        {
            Author = author != null ? author : null;
            _rb.velocity = trajectory;
            _rb.angularVelocity = angularVelocity;
            DetectHit();
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
