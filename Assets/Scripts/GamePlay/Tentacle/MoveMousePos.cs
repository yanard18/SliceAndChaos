using UnityEngine;

namespace DenizYanar
{
    public class MoveMousePos : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float _speed = 100.0f;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (Camera.main is null) return;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var position = transform.position;
            var dir = mousePos - position;
            var targetPos = position + dir.normalized * (_speed * Time.fixedDeltaTime);
            _rb.MovePosition(targetPos);
        }
    }
}
