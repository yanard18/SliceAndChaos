using System;
using System.Collections;
using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using DenizYanar.FSM;
using DenizYanar.Inputs;
using UnityEngine;


namespace DenizYanar.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        
        private Rigidbody2D _rb;
        private Collider2D _collider;
        private StateMachine _stateMachine;
        
        private bool _rememberedJumpRequest;

        [Header("Player Settings")]
        [SerializeField] private PlayerSettings _settings;
        
        [Header("Player State Informer Channel")]
        [SerializeField] private StringEventChannelSO _stateNameInformerEvent;

        [Header("Senses")]
        [SerializeField] private SenseEnginePlayer _jumpSense;
        
        public JumpData JumpDataInstance { get; private set; }
        public WallSlideData WallSlideDataInstance { get; private set; }


        
        [SerializeField] private PlayerInputs _inputs;
        

        
        
        

        #region Monobehaviour
        private void Awake()
        {
            _collider = GetComponentInChildren<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
            
            JumpDataInstance = new JumpData(2, 20, _rb);
            WallSlideDataInstance = new WallSlideData(_rb, _collider);
            
            _stateMachine = new StateMachine();

            var idle = new PlayerMovementIdleState(_rb, nameInformerEvent: _stateNameInformerEvent, stateName: "Idle");
            var move = new PlayerMovementMoveState(_rb, _settings, _inputs, nameInformerEvent: _stateNameInformerEvent, stateName: "Move");
            var jump = new PlayerMovementJumpState(this, _jumpSense, nameInformerChannel: _stateNameInformerEvent, stateName: "Jump");
            var land = new PlayerMovementLandState(JumpDataInstance, nameInformerEvent: _stateNameInformerEvent, stateName: "Land");
            var wallSlide = new PlayerMovementWallSlideState(this, _settings,nameInformerEventChannel: _stateNameInformerEvent, stateName: "Wall Slide");
            var air = new PlayerMovementAirState(_rb, _settings, _inputs, nameInformerChannel: _stateNameInformerEvent, stateName: "At Air");
            var shift = new PlayerMovementShiftState(_rb, _settings, _inputs, nameInformerEvent: _stateNameInformerEvent, stateName: "Shift");
            var slice = new PlayerMovementSliceState(_rb, _settings, _inputs);

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
            
            Func<bool> HasMovementInput() => () => Mathf.Abs(_inputs.HorizontalMovement) > 0;
            Func<bool> HasNotMovementInput() => () => _inputs.HorizontalMovement == 0;
            Func<bool> CanJump() => () =>  _rememberedJumpRequest && JumpDataInstance.CanJump;
            Func<bool> WhenJumpKeyTriggered() => () => _rememberedJumpRequest;
            Func<bool> OnFallToGround() => () => IsTouchingToGround() != null && _rb.velocity.y <= 0;
            Func<bool> NoMoreContactToGround() => () => IsTouchingToGround() == null;
            Func<bool> OnContactToWall() => () => AngleOfContact() == 0 && WallSlideDataInstance.HasCooldown == false;
            Func<bool> NoContactToWall() => () => AngleOfContact() == null || AngleOfContact() != 0;
            Func<bool> OnPressedShift() => () => _inputs.Shift;
            Func<bool> OnPressedLeftClick() => () => _inputs.Attack1;
            Func<bool> OnSliceFinished() => () => slice.HasFinished;
            Func<bool> AlwaysTrue() => () => true;

        }

        private void Update()
        {
            _stateMachine.Tick();
            
            if (_inputs.Jump)
                StartCoroutine(RememberJumpRequest(0.15f));
        }
        private void FixedUpdate() => _stateMachine.PhysicsTick();

        #endregion

        #region Local Methods



        private float? IsTouchingToGround()
        {
            const int rayCount = 8;
            var bounds = _collider.bounds;
            var spaceBetweenRays = bounds.size.x / (rayCount - 1);
            var bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            for (var i = 0; i < 8; i++)
            {
                var hit = Physics2D.Raycast(
                    bottomLeft + Vector2.right * (spaceBetweenRays * i), 
                    Vector2.down, 0.1f, _settings.ObstacleLayerMask);

                if (hit)
                    return Vector2.Angle(hit.normal, Vector2.up) % 90f;
            }

            return null;
        }
        
        private RaycastHit2D? IsTouchingToWall()
        {
            if(_inputs.HorizontalMovement == 0)
                return null;

            var bounds = _collider.bounds;
            const int horizontalRayCount = 2;
            var verticalRaySpace = bounds.size.y / (horizontalRayCount - 1);
            var movementDirection = _inputs.HorizontalMovement > 0 ? 1 : -1;

            var rayStartPosition = movementDirection == 1 ? new Vector2(bounds.max.x, bounds.min.y) : new Vector2(bounds.min.x, bounds.min.y);

            for (var i = 0; i < 2; i++)
            {
                Debug.DrawRay(rayStartPosition + Vector2.up * verticalRaySpace * i, Vector2.right * movementDirection,
                    Color.red);
                
                
                var hit = Physics2D.Raycast(
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
            if (hit == null) 
                return null;
            
            var angle = Vector2.Angle(hit.Value.normal, Vector2.up);
            angle %= 90;
            return angle;

        }

        private IEnumerator RememberJumpRequest(float duration)
        {
            // Change jump input in a bad way, but that's prevent the holding jump key.
            _inputs.Jump = false;
            
            if (_rememberedJumpRequest)
                yield return null;

            _rememberedJumpRequest = true;
            yield return new WaitForSeconds(duration);
            _rememberedJumpRequest = false;
        }

        #endregion
        
        
        

    }
    
}
