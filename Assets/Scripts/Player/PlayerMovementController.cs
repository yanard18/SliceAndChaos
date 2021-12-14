using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Collider2D _collider;

        private float _horizontalMovementInput;


        [SerializeField] private PlayerSettings _settings;
        
        
        //Jump--------
        private bool _hasJumpDelay;
        private int _existJumpCount = 2;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponentInChildren<Collider2D>();
        }
        
        private bool CanJump => !_hasJumpDelay && _existJumpCount > 0;

        private void Update()
        {
            if (IsTouchingGround() && !_hasJumpDelay)
                _existJumpCount = _settings.JumpCount;

        }

        private void Jump()
        {
            if(!CanJump)
                return;
            
            _existJumpCount--;
            _rb.velocity = new Vector2(_rb.velocity.x, _settings.JumpForce);
            StartCoroutine(StartJumpDelay(_settings.JumpDelayDuration));
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_horizontalMovementInput * 10f, _rb.velocity.y);
        }

        public void ReadHorizontalMovementInput(InputAction.CallbackContext context)
        {
            _horizontalMovementInput = context.ReadValue<float>();
        }

        public void ReadJumpInput(InputAction.CallbackContext context)
        {
            if(context.started)
                Jump();
        }
        
        private bool IsTouchingGround()
        {
            Bounds bounds = _collider.bounds;
            float spaceBetweenRays = bounds.size.x;
            Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            for (int i = 0; i < 2; i++)
            {
                bool isGrounded = Physics2D.Raycast(
                    bottomLeft + Vector2.right * (spaceBetweenRays * i), 
                Vector2.down, 0.1f, _settings.ObstacleLayerMask);

                if (isGrounded)
                    return true;
            }

            return false;
        }


        private IEnumerator StartJumpDelay(float duration)
        {
            _hasJumpDelay = true;
            yield return new WaitForSeconds(duration);
            _hasJumpDelay = false;
        }

    }
}
