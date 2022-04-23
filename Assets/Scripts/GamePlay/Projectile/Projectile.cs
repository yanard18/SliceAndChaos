using UnityEngine;

namespace DenizYanar.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        private Rigidbody2D m_Rb;
        private bool m_Hit;

        [SerializeField]
        private LayerMask m_HitBoxLayer;

        [SerializeField]
        private bool m_bIsDebugEnabled;

        public GameObject Author { get; private set; }

        #region Monobehaviour

        protected virtual void Awake() => m_Rb = GetComponent<Rigidbody2D>();

        protected virtual void FixedUpdate() => DetectHit();

        #endregion

        #region Private Methods

        private void DetectHit()
        {
            if (m_Hit) return;

            var velocity = m_Rb.velocity;
            var currentPosition = m_Rb.position;
            var desiredVelocityVector = velocity * Time.fixedDeltaTime;

            // ReSharper disable once Unity.PreferNonAllocApi
            RaycastHit2D[] hit = Physics2D.CircleCastAll(
                currentPosition,
                0.1f,
                desiredVelocityVector.normalized,
                desiredVelocityVector.magnitude,
                m_HitBoxLayer);


#if UNITY_EDITOR
            if (m_bIsDebugEnabled)
                Debug.DrawRay(currentPosition, desiredVelocityVector, Color.magenta, 5.0f);
#endif

            if (hit.Length <= 0) return;

            foreach (var t in hit)
            {
#if UNITY_EDITOR
                if (m_bIsDebugEnabled)
                    Debug.Log(t.transform.name);
#endif

                if (t.transform.gameObject == Author) continue;

                Hit(t.collider);
                m_Hit = true;
                transform.position = t.point;

#if UNITY_EDITOR
                if (m_bIsDebugEnabled)
                    Debug.Log("Hit to: " + t.transform.name);
#endif

                return;
            }
        }

        #endregion

        protected void StopProjectile()
        {
            m_Rb.velocity = Vector2.zero;
            m_Rb.angularVelocity = 0f;
        }

        protected abstract void Hit(Collider2D col);


        #region Public Methods

        public void Init(Vector2 trajectory, float angularVelocity = 0, float lifeTime = 5.0f, GameObject author = null)
        {
            Author = author != null ? author : null;
            m_Rb.velocity = trajectory;
            m_Rb.angularVelocity = angularVelocity;
            DetectHit();
            if (lifeTime > 0f)
                Destroy(gameObject, lifeTime);
        }

        #endregion
    }
}