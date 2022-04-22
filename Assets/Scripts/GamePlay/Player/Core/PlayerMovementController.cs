using System;
using System.Collections;
using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using DenizYanar.FSM;
using UnityEngine;



namespace DenizYanar.PlayerSystem.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        #region Private Variables

        private Rigidbody2D _rb;
        private StateMachine _stateMachine;
        
        private bool _rememberedJumpRequest;
        
        #endregion

        #region Private State Variables

        private IdleState _idle;
        private MoveState _move;
        private JumpState _jump;
        private ShiftState _shift;
        private TeleportState _teleport;
        private AirState _air;
        private LandState _land;
        private WallSlideState _wallSlide;
        

        #endregion

        #region Serialized Variables

        [Header("Player Settings")]
        [SerializeField] private PlayerSettings _settings;
        
        [Header("Player Inputs")]
        [SerializeField] private PlayerInputs _inputs;
        
        [Header("Player State Informer Channel")]
        [SerializeField] private StringEventChannelSO _stateTitleEvent;
        
        [Header("Dependencies")]
        [SerializeField] private Collider2D _collider;
        
        [Header("Senses")]
        [SerializeField] private SenseEnginePlayer _jumpSense;

        [SerializeField] private SenseEnginePlayer _landSense;
        [SerializeField] private SenseEnginePlayer _enterShiftSense;
        [SerializeField] private SenseEnginePlayer _leaveShiftSense;
        

        #endregion

        #region Public Variables

        public JumpProperties JumpPropertiesInstance { get; private set; }
        public WallSlideData WallSlideDataInstance { get; private set; }

        #endregion

        #region Monobehaviour

        private void OnEnable()
        {
            _inputs.e_OnJumpStarted += OnJumpStarted;
            _inputs.e_OnShiftStarted += OnShiftStarted;
            _inputs.e_OnAttack1Started += OnAttack1Started;
        }

        private void OnDisable()
        {
            _inputs.e_OnJumpStarted -= OnJumpStarted;
            _inputs.e_OnShiftStarted -= OnShiftStarted;
            _inputs.e_OnAttack1Started -= OnAttack1Started;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            SetupStateMachine();
        }

        private void SetupStateMachine()
        {
            JumpPropertiesInstance = new JumpProperties(2, 20, _rb);
            WallSlideDataInstance = new WallSlideData(_rb, _collider);

            _stateMachine = new StateMachine();

            _idle = new IdleState(_rb, nameInformerEvent: _stateTitleEvent, stateName: "Idle");
            _move = new MoveState(_rb, _settings, _inputs, nameInformerEvent: _stateTitleEvent, stateName: "Move");
            _jump = new JumpState(this, _jumpSense, nameInformerChannel: _stateTitleEvent, stateName: "Jump");
            _land = new LandState(JumpPropertiesInstance, _landSense, nameInformerEvent: _stateTitleEvent, stateName: "Land");
            _wallSlide = new WallSlideState(this, _settings, nameInformerEventChannel: _stateTitleEvent, stateName: "Wall Slide");
            _air = new AirState(_rb, _settings, _inputs, nameInformerChannel: _stateTitleEvent, stateName: "At Air");
            _shift = new ShiftState(_rb, _settings, _enterShiftSense, _leaveShiftSense, nameInformerEvent: _stateTitleEvent, stateName: "Shift");
            _teleport = new TeleportState(_rb, _settings);

            _stateMachine.InitState(_idle);

            To(_idle, _move, HasMovementInput());
            To(_move, _idle, HasNotMovementInput());
            To(_idle, _jump, CanJump());
            To(_move, _jump, CanJump());
            To(_idle, _air, NoMoreContactToGround());
            To(_move, _air, NoMoreContactToGround());
            To(_air, _land, OnFallToGround());
            To(_air, _jump, CanJump());
            To(_land, _idle, AlwaysTrue());
            To(_air, _wallSlide, OnContactToWall());
            To(_wallSlide, _air, WhenJumpKeyTriggered());
            To(_wallSlide, _air, NoContactToWall());
            To(_teleport, _air, OnSliceFinished());
            To(_air, _shift, () => false);
            To(_shift, _air, () => false);
            To(_shift, _teleport, () => false);
            To(_jump, _air, () => true);

            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(@from, to, condition);

            Func<bool> HasMovementInput() => () => Mathf.Abs(_inputs.m_HorizontalMovement) > 0;
            Func<bool> HasNotMovementInput() => () => _inputs.m_HorizontalMovement == 0;
            Func<bool> CanJump() => () => _rememberedJumpRequest && JumpPropertiesInstance.CanJump;
            Func<bool> WhenJumpKeyTriggered() => () => _rememberedJumpRequest;
            Func<bool> OnFallToGround() => () => IsTouchingToGround() != null && _rb.velocity.y <= 0;
            Func<bool> NoMoreContactToGround() => () => IsTouchingToGround() == null;
            Func<bool> OnContactToWall() => () => AngleOfContact() == 0 && WallSlideDataInstance.HasCooldown == false;
            Func<bool> NoContactToWall() => () => AngleOfContact() == null || AngleOfContact() != 0;
            Func<bool> OnSliceFinished() => () => _teleport.HasFinished;
            Func<bool> AlwaysTrue() => () => true;
        }

        private void Update() => _stateMachine.Tick();
        private void FixedUpdate() => _stateMachine.PhysicsTick();

        #endregion

        #region Inputs

        private void OnJumpStarted() => StartCoroutine(RememberJumpRequest(0.15f));

        private void OnAttack1Started() => _stateMachine.TriggerState(_teleport);
        
        private void OnShiftStarted()
        {
            if(_stateMachine.TriggerState(_shift))
                return;
            
            _stateMachine.TriggerState(_air);
        }
        
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
            if(_inputs.m_HorizontalMovement == 0)
                return null;

            var bounds = _collider.bounds;
            const int horizontalRayCount = 2;
            var verticalRaySpace = bounds.size.y / (horizontalRayCount - 1);
            var movementDirection = _inputs.m_HorizontalMovement > 0 ? 1 : -1;

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
            //_inputs.Jump = false;
            
            if (_rememberedJumpRequest)
                yield return null;

            _rememberedJumpRequest = true;
            yield return new WaitForSeconds(duration);
            _rememberedJumpRequest = false;
        }

        #endregion
        

    }
    
}
