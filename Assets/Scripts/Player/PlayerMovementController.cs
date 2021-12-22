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

        private bool _wallSlideEnabled = false;
        private float _defaultRigidbodyGravityValue;
        private const float _wallSlideGravityValue = 0.5f;

        private float _horizontalMovementInput;

        private float _xVelocity;


        [SerializeField] private PlayerSettings _settings;
        
        
        //Jump--------
        private bool _hasJumpDelay;
        private int _existJumpCount = 2;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponentInChildren<Collider2D>();

            _defaultRigidbodyGravityValue = _rb.gravityScale;
            
            

        }
        
        private bool CanJump => !_hasJumpDelay && _existJumpCount > 0;

        private void Update()
        {
            HandleJumpReset();
            HandleWallSlide();
        }

        private void HandleJumpReset()
        {
            if (IsTouchingGroundAndAngle() == 0 && !_hasJumpDelay)
                _existJumpCount = _settings.JumpCount;
        }

        private void Jump()
        {
            if(!CanJump)
                return;

            if (_wallSlideEnabled)
            {
                WallSlideJump();
                return;;
            }


            _existJumpCount--;
            _rb.velocity = new Vector2(_rb.velocity.x, _settings.JumpForce);
            StartCoroutine(StartJumpDelay(_settings.JumpDelayDuration));
        }

        private void WallSlideJump()
        {
            _rb.velocity = Vector2.left * 100;
        }

        private void HandleWallSlide()
        {

            if (IsTouchingToWallAndAngle() == 0)
            {
                if(!_wallSlideEnabled)
                    EnableWallSlide();
            }
            else
            {
                if(_wallSlideEnabled)
                    DisableWallSlide();
            }

        }

        private void EnableWallSlide()
        {
            _wallSlideEnabled = true;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y / 5f);
            _rb.gravityScale = _wallSlideGravityValue;
        }
        
        private void DisableWallSlide()
        {
            _wallSlideEnabled = false;
            _rb.gravityScale = _defaultRigidbodyGravityValue;
        }

        private void FixedUpdate()
        {
            if (IsTouchingToWallAndAngle() > 45.0f)
            {
                return;
            }


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
            var spaceBetweenRays = bounds.size.x;
            Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            for (var i = 0; i < 2; i++)
            {
                bool isGrounded = Physics2D.Raycast(
                    bottomLeft + Vector2.right * (spaceBetweenRays * i), 
                Vector2.down, 0.1f, _settings.ObstacleLayerMask);

                if (isGrounded)
                    return true;
            }

            return false;
        }

        private float? IsTouchingGroundAndAngle()
        {
            Bounds bounds = _collider.bounds;
            var spaceBetweenRays = bounds.size.x;
            Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            for (var i = 0; i < 2; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    bottomLeft + Vector2.right * (spaceBetweenRays * i), 
                    Vector2.down, 0.1f, _settings.ObstacleLayerMask);

                if (hit)
                    return Vector2.Angle(hit.normal, Vector2.up) % 90f;
            }

            return null;
        }
        
        private bool IsTouchingToWall()
        {
            if(_horizontalMovementInput == 0)
                return false;


            Bounds bounds = _collider.bounds;
            var horizontalRayCount = 2;
            var verticalRaySpace = bounds.size.y / (horizontalRayCount - 1);
            var movementDirection = _horizontalMovementInput > 0 ? 1 : -1;

            Vector2 rayStartPosition = new Vector2();
            
            rayStartPosition = movementDirection == 1 ? new Vector2(bounds.max.x, bounds.min.y) : new Vector2(bounds.min.x, bounds.min.y);

            for (var i = 0; i < 2; i++)
            {
                Debug.DrawRay(rayStartPosition + Vector2.up * verticalRaySpace * i, Vector2.right * movementDirection,
                    Color.red);
                
                
                RaycastHit2D hit = Physics2D.Raycast(
                    rayStartPosition + Vector2.up * (verticalRaySpace * i),
                    Vector2.right * movementDirection,
                    0.1f,
                    _settings.ObstacleLayerMask);
                
                
                if (hit)
                    return true;

            }
            
            return false;
        }
        
        private float? IsTouchingToWallAndAngle()
        {
            if(_horizontalMovementInput == 0)
                return null;

            Bounds bounds = _collider.bounds;
            const int horizontalRayCount = 2;
            var verticalRaySpace = bounds.size.y / (horizontalRayCount - 1);
            var movementDirection = _horizontalMovementInput > 0 ? 1 : -1;

            Vector2 rayStartPosition = new Vector2();
            
            rayStartPosition = movementDirection == 1 ? new Vector2(bounds.max.x, bounds.min.y) : new Vector2(bounds.min.x, bounds.min.y);

            for (var i = 0; i < 2; i++)
            {
                Debug.DrawRay(rayStartPosition + Vector2.up * verticalRaySpace * i, Vector2.right * movementDirection,
                    Color.red);
                
                
                RaycastHit2D hit = Physics2D.Raycast(
                    rayStartPosition + Vector2.up * (verticalRaySpace * i),
                    Vector2.right * movementDirection,
                    0.1f,
                    _settings.ObstacleLayerMask);
                
                if (hit)
                {
                    var angle = Vector2.Angle(hit.normal, Vector2.up) % 90f;
                    return angle;
                }

            }
            
            return null;

        }


        private IEnumerator StartJumpDelay(float duration)
        {
            _hasJumpDelay = true;
            yield return new WaitForSeconds(duration);
            _hasJumpDelay = false;
        }

    }
}
