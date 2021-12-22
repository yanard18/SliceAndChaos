using System;
using System.Collections;
using DenizYanar.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        
        private Rigidbody2D _rb;
        private Collider2D _collider;

        [SerializeField] private PlayerSettings _settings;

        [SerializeField] private StringEventChannelSO _stateNameInformerEvent;

        public JumpData JumpDataInstance;
        public WallSlideData WallSlideDataInstance;

        private StateMachine _stateMachine;

        private bool _rememberedJumpRequest;
        
        
        
        private void Awake()
        {

            _collider = GetComponentInChildren<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
            
            JumpDataInstance = new JumpData(2, 20, _rb);
            WallSlideDataInstance = new WallSlideData(_rb, _collider);
            
            
            _stateMachine = new StateMachine();

            PlayerIdleState idle = new PlayerIdleState(_rb, nameInformerEvent: _stateNameInformerEvent, stateName: "Idle");
            PlayerMoveState move = new PlayerMoveState(_rb, _settings, nameInformerEvent: _stateNameInformerEvent, stateName: "Move");
            PlayerJumpState jump = new PlayerJumpState(this, nameInformerChannel: _stateNameInformerEvent, stateName: "Jump");
            PlayerLandState land = new PlayerLandState(JumpDataInstance, nameInformerEvent: _stateNameInformerEvent, stateName: "Land");
            PlayerWallSlideState wallSlide = new PlayerWallSlideState(this, _settings,nameInformerEventChannel: _stateNameInformerEvent, stateName: "Wall Slide");
            PlayerAirState air = new PlayerAirState(_rb, _settings, nameInformerChannel: _stateNameInformerEvent, stateName: "At Air");
            PlayerShiftState shift = new PlayerShiftState(_rb, _settings, nameInformerEvent: _stateNameInformerEvent, stateName: "Shift");

            _stateMachine.InitState(idle);

           
            
            
            /* old machine
            To(idle, move, HasMovementInput());
            To(move, idle, HasNotMovementInput());
            To(idle, jump, CanJump());
            To(move, jump, CanJump());
            To(move, fall, IsFallingAndNotTouchingGround());
            To(move,rise, IsRisingAndNotTouchingGround());
            To(jump, rise, AlwaysTrue());
            To(rise, fall, IsFalling());
            To(rise, jump, CanJump());
            To(fall, jump, CanJump());
            To(airStrafe, jump, CanJump());
            To(fall, land, OnContactToGround());
            To(land, idle, HasNotMovementInput());
            To(land, move, HasMovementInput());
            To(rise, airStrafe, HasMovementInput());
            To(fall, airStrafe, HasMovementInput());
            To(airStrafe, fall, FallingAndNotMoving());
            To(airStrafe, rise, RisingAndNotMoving());
            To(airStrafe, land, OnContactToGround());
            To(airStrafe, wallSlide, OnContactToWall());
            To(wallSlide, rise, WhenJumpKeyTriggered());
            To(airStrafe, dive, ShouldDive());
            To(rise, dive, ShouldDive());
            To(fall, dive, ShouldDive());
            To(dive, land, OnContactToGround());
            To(dive,rise, HasNoMovementAndRising());
            To(dive, fall, HasNoMovementAndFalling());*/
            
            To(idle, move, HasMovementInput());
            To(move,idle, HasNotMovementInput());
            To(idle, jump, CanJump());
            To(move, jump, CanJump());
            To(jump, air, AlwaysTrue());
            To(air, land, OnContactToGround());
            To(air, jump, CanJump());
            To(land, idle, AlwaysTrue());
            To(air, wallSlide, OnContactToWall());
            To(wallSlide, air, WhenJumpKeyTriggered());
            To(wallSlide, air, NoContactToWall());
            To(air, shift, OnPressedShift());
            To(shift, air, OnPressedShift());
            





            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            

            /* old machine
            Func<bool> HasMovementInput() => () => Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0;
            Func<bool> HasNotMovementInput() => () => Input.GetAxisRaw("Horizontal") == 0;
            Func<bool> CanJump() => () =>  _rememberedJumpRequest && JumpDataInstance.CanJump;
            Func<bool> WhenJumpKeyTriggered() => () => _rememberedJumpRequest;
            Func<bool> IsFalling() => () => _rb.velocity.y < 0;
            Func<bool> IsFallingAndNotTouchingGround() => () => _rb.velocity.y < 0 && IsTouchingToGround() == null;
            Func<bool> IsRisingAndNotTouchingGround() => () => _rb.velocity.y > 0 && IsTouchingToGround() == null;
            Func<bool> HasNoMovementAndRising() => () => _rb.velocity.y > 0 && Input.GetKey(KeyCode.S) == false;
            Func<bool> HasNoMovementAndFalling() => () => _rb.velocity.y < 0 && Input.GetKey(KeyCode.S) == false;
            Func<bool> OnContactToGround() => () => IsTouchingToGround() != null && _rb.velocity.y <= 0;
            Func<bool> FallingAndNotMoving() => () => _rb.velocity.y < 0 && Input.GetAxisRaw("Horizontal") == 0;
            Func<bool> RisingAndNotMoving() => () => _rb.velocity.y > 0 && Input.GetAxis("Horizontal") == 0;
            Func<bool> OnContactToWall() => () => AngleOfContact() == 0 && WallSlideDataInstance.HasCooldown == false;
            Func<bool> NoContactToWall() => () => AngleOfContact() == null || AngleOfContact() != 0;
            Func<bool> AlwaysTrue() => () => true;
            Func<bool> ShouldDive() => () => Input.GetKey(KeyCode.S);
            */
            
            Func<bool> HasMovementInput() => () => Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0;
            Func<bool> HasNotMovementInput() => () => Input.GetAxisRaw("Horizontal") == 0;
            Func<bool> CanJump() => () =>  _rememberedJumpRequest && JumpDataInstance.CanJump;
            Func<bool> WhenJumpKeyTriggered() => () => _rememberedJumpRequest;
            Func<bool> OnContactToGround() => () => IsTouchingToGround() != null && _rb.velocity.y <= 0;
            Func<bool> OnContactToWall() => () => AngleOfContact() == 0 && WallSlideDataInstance.HasCooldown == false;
            Func<bool> NoContactToWall() => () => AngleOfContact() == null || AngleOfContact() != 0;
            Func<bool> OnPressedShift() => () => Input.GetKeyDown(KeyCode.LeftShift);
            Func<bool> AlwaysTrue() => () => true;

        }

        private void Update()
        {
            _stateMachine.Tick();

            if (Input.GetKeyDown(KeyCode.W))
                StartCoroutine(RememberJumpRequest(0.15f));
        }
        private void FixedUpdate() => _stateMachine.PhysicsTick();

        
        private float? IsTouchingToGround()
        {
            const int rayCount = 8;
            Bounds bounds = _collider.bounds;
            var spaceBetweenRays = bounds.size.x / (rayCount - 1);
            Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            for (var i = 0; i < 8; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    bottomLeft + Vector2.right * (spaceBetweenRays * i), 
                    Vector2.down, 0.1f, _settings.ObstacleLayerMask);

                if (hit)
                    return Vector2.Angle(hit.normal, Vector2.up) % 90f;
            }

            return null;
        }
        
        private RaycastHit2D? IsTouchingToWall()
        {
            if(Input.GetAxisRaw("Horizontal") == 0)
                return null;

            Bounds bounds = _collider.bounds;
            const int horizontalRayCount = 2;
            var verticalRaySpace = bounds.size.y / (horizontalRayCount - 1);
            var movementDirection = Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1;

            Vector2 rayStartPosition = movementDirection == 1 ? new Vector2(bounds.max.x, bounds.min.y) : new Vector2(bounds.min.x, bounds.min.y);

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
                    return hit;
            }
            
            return null;
        }

        private float? AngleOfContact()
        {
            RaycastHit2D? hit = IsTouchingToWall();
            if (hit != null)
            {
                var angle = Vector2.Angle(hit.Value.normal, Vector2.up);
                angle %= 90;
                return angle;
            }

            return null;
        }

        private IEnumerator RememberJumpRequest(float duration)
        {
            if (_rememberedJumpRequest == true)
                yield return null;

            _rememberedJumpRequest = true;
            yield return new WaitForSeconds(duration);
            _rememberedJumpRequest = false;
        }




    }
}
