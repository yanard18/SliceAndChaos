using UnityEngine;

namespace DenizYanar
{
    public class MoveMousePos : MonoBehaviour
    {
        private Rigidbody2D m_Rb;
        [SerializeField]
        private float m_Speed = 100.0f;
        
        private void Awake()
        {
            m_Rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (Camera.main is null) return;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var position = transform.position;
            var dir = mousePos - position;
            var targetPos = position + dir.normalized * (m_Speed * Time.fixedDeltaTime);
            m_Rb.MovePosition(targetPos);
        }
    }
}
