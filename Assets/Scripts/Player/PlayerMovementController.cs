using System;
using System.Collections;
using DenizYanar.Events;
using DenizYanar.FSM;
using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        
        private Rigidbody2D _rb;
        private Collider2D _collider;

        [SerializeField] private PlayerSettings _settings;

        [SerializeField] private StringEventChannelSO _stateNameInformerEvent;

        public JumpData JumpDataInstance;
        public WallSlideData WallSlideDataInstance;

        private StateMachine _stateMachine;
        public State ActiveState => _stateMachine.CurrentState;

        private bool _rememberedJumpRequest;
        
        
        
        private void Awake()
        {

            _collider = GetComponentInChildren<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
            
            JumpDataInstance = new JumpData(2, 20, _rb);
            WallSlideDataInstance = new WallSlideData(_rb, _collider);
            
            
            _stateMachine = new StateMachine();

            PlayerMovementIdleState idle = new PlayerMovementIdleState(_rb, nameInformerEvent: _stateNameInformerEvent, stateName: "Idle");
            PlayerMovementMoveState move = new PlayerMovementMoveState(_rb, _settings, nameInformerEvent: _stateNameInformerEvent, stateName: "Move");
            PlayerMovementJumpState jump = new PlayerMovementJumpState(this, nameInformerChannel: _stateNameInformerEvent, stateName: "Jump");
            PlayerMovementLandState land = new PlayerMovementLandState(JumpDataInstance, nameInformerEvent: _stateNameInformerEvent, stateName: "Land");
            PlayerMovementWallSlideState wallSlide = new PlayerMovementWallSlideState(this, _settings,nameInformerEventChannel: _stateNameInformerEvent, stateName: "Wall Slide");
            PlayerMovementAirState air = new PlayerMovementAirState(_rb, _settings, nameInformerChannel: _stateNameInformerEvent, stateName: "At Air");
            PlayerMovementShiftState shift = new PlayerMovementShiftState(_rb, _settings, nameInformerEvent: _stateNameInformerEvent, stateName: "Shift");
            PlayerMovementSliceState slice = new PlayerMovementSliceState(_rb);

            _stateMachine.InitState(idle);

           
            
            
            
            
            To(idle, move, HasMovementInput());
            To(move,idle, HasNotMovementInput());
            To(idle, jump, CanJump());
            To(move, jump, CanJump());
            To(jump, air, AlwaysTrue());
            To(idle, air, NoMoreContactToGround());
            To(move, air, NoMoreContactToGround());
            To(air, land, OnFallToGround());
            To(air, jump, CanJump());
            To(land, idle, AlwaysTrue());
            To(air, wallSlide, OnContactToWall());
            To(wallSlide, air, WhenJumpKeyTriggered());
            To(wallSlide, air, NoContactToWall());
            To(air, shift, OnPressedShift());
            To(shift, air, OnPressedShift());
            To(shift, slice, OnPressedLeftClick());
            To(slice, air, OnSliceFinished());
            





            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            

            Func<bool> HasMovementInput() => () => Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0;
            Func<bool> HasNotMovementInput() => () => Input.GetAxisRaw("Horizontal") == 0;
            Func<bool> CanJump() => () =>  _rememberedJumpRequest && JumpDataInstance.CanJump;
            Func<bool> WhenJumpKeyTriggered() => () => _rememberedJumpRequest;
            Func<bool> OnFallToGround() => () => IsTouchingToGround() != null && _rb.velocity.y <= 0;
            Func<bool> NoMoreContactToGround() => () => IsTouchingToGround() == null;
            Func<bool> OnContactToWall() => () => AngleOfContact() == 0 && WallSlideDataInstance.HasCooldown == false;
            Func<bool> NoContactToWall() => () => AngleOfContact() == null || AngleOfContact() != 0;
            Func<bool> OnPressedShift() => () => Input.GetKeyDown(KeyCode.LeftShift);
            Func<bool> OnPressedLeftClick() => () => Input.GetMouseButtonDown(0);
            Func<bool> OnSliceFinished() => () => slice.HasFinished;
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
            if (_rememberedJumpRequest)
                yield return null;

            _rememberedJumpRequest = true;
            yield return new WaitForSeconds(duration);
            _rememberedJumpRequest = false;
        }

    }
}
