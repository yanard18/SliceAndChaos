using System;
using System.Collections;
using DenizYanar.Events;
using DenizYanar.SenseEngine;
using DenizYanar.FSM;
using DenizYanar.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;



namespace DenizYanar.PlayerSystem.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        #region Private Variables

        private Rigidbody2D m_Rb;
        private StateMachine m_StateMachine;
        
        private bool m_bHasJumpRequest;
        
        #endregion

        #region Private State Variables

        private IdleState m_sIdle;
        private MoveState m_sMove;
        private JumpState m_sJump;
        private ShiftState m_sShift;
        private TeleportState m_sTeleport;
        private AirState m_sAir;
        private LandState m_sLand;
        private WallSlideState m_sWallSlide;
        

        #endregion

        #region Serialized Variables

        [Header("Player Settings")]
        
        [SerializeField] [Required]
        private PlayerSettings m_Settings;
        
        [Header("Player Inputs")]   
        
        [SerializeField] [Required]
        private PlayerInputs m_Inputs;
        
        [Header("Player State Informer Channel")]   
        
        [SerializeField] [Required]
        private StringEvent m_ecStateChangeTitle;
        
        [Header("Dependencies")]
        
        [SerializeField] [Required]
        private Collider2D m_PlayerCollision;
        
        [Header("Senses")]
        
        [SerializeField] 
        private SenseEnginePlayer m_sepJump;

        [SerializeField]
        private SenseEnginePlayer m_sepLand;
        
        [SerializeField]
        private SenseEnginePlayer m_sepEnterShift;
        
        [SerializeField]
        private SenseEnginePlayer m_sepLeaveShift;
        

        #endregion

        #region Public Variables

        public JumpProperties JumpPropertiesInstance { get; private set; }
        public WallSlideData WallSlideDataInstance { get; private set; }

        #endregion

        #region Monobehaviour

        private void OnEnable()
        {
            m_Inputs.e_OnJumpStarted += OnJumpStarted;
            m_Inputs.e_OnShiftStarted += OnShiftStarted;
            m_Inputs.e_OnAttack1Started += OnAttack1Started;
        }

        private void OnDisable()
        {
            m_Inputs.e_OnJumpStarted -= OnJumpStarted;
            m_Inputs.e_OnShiftStarted -= OnShiftStarted;
            m_Inputs.e_OnAttack1Started -= OnAttack1Started;
        }

        private void Awake()
        {
            m_Rb = GetComponent<Rigidbody2D>();
            
            SetupStateMachine();
        }

        private void SetupStateMachine()
        {
            JumpPropertiesInstance = new JumpProperties(2, 20, m_Rb);
            WallSlideDataInstance = new WallSlideData(m_Rb, m_PlayerCollision);

            m_StateMachine = new StateMachine();

            m_sIdle = new IdleState(m_Rb, nameInformerEvent: m_ecStateChangeTitle, stateName: "Idle");
            m_sMove = new MoveState(m_Rb, m_Settings, m_Inputs, nameInformerEvent: m_ecStateChangeTitle, stateName: "Move");
            m_sJump = new JumpState(this, m_sepJump, nameInformerChannel: m_ecStateChangeTitle, stateName: "Jump");
            m_sLand = new LandState(JumpPropertiesInstance, m_sepLand, nameInformerEvent: m_ecStateChangeTitle, stateName: "Land");
            m_sWallSlide = new WallSlideState(this, m_Settings, name: m_ecStateChangeTitle, stateName: "Wall Slide");
            m_sAir = new AirState(m_Rb, m_Settings, m_Inputs, nameInformerChannel: m_ecStateChangeTitle, stateName: "At Air");
            m_sShift = new ShiftState(m_Rb, m_Inputs, m_Settings, m_sepEnterShift, m_sepLeaveShift, nameInformerEvent: m_ecStateChangeTitle, stateName: "Shift");
            m_sTeleport = new TeleportState(m_Rb, m_Settings);

            m_StateMachine.InitState(m_sIdle);

            To(m_sIdle, m_sMove, HasMovementInput());
            To(m_sMove, m_sIdle, HasNotMovementInput());
            To(m_sIdle, m_sJump, CanJump());
            To(m_sMove, m_sJump, CanJump());
            To(m_sIdle, m_sAir, NoMoreContactToGround());
            To(m_sMove, m_sAir, NoMoreContactToGround());
            To(m_sAir, m_sLand, OnFallToGround());
            To(m_sAir, m_sJump, CanJump());
            To(m_sLand, m_sIdle, AlwaysTrue());
            To(m_sAir, m_sWallSlide, OnContactToWall());
            To(m_sWallSlide, m_sAir, WhenJumpKeyTriggered());
            To(m_sWallSlide, m_sAir, NoContactToWall());
            To(m_sTeleport, m_sAir, OnSliceFinished());
            To(m_sAir, m_sShift, () => false);
            To(m_sShift, m_sAir, () => false);
            To(m_sShift, m_sTeleport, () => false);
            To(m_sJump, m_sAir, () => true);

            void To(State from, State to, Func<bool> condition) => m_StateMachine.AddTransition(@from, to, condition);

            Func<bool> HasMovementInput() => () => Mathf.Abs(m_Inputs.m_HorizontalMovement) > 0;
            Func<bool> HasNotMovementInput() => () => m_Inputs.m_HorizontalMovement == 0;
            Func<bool> CanJump() => () => m_bHasJumpRequest && JumpPropertiesInstance.CanJump;
            Func<bool> WhenJumpKeyTriggered() => () => m_bHasJumpRequest;
            Func<bool> OnFallToGround() => () => IsTouchingToGround() != null && m_Rb.velocity.y <= 0;
            Func<bool> NoMoreContactToGround() => () => IsTouchingToGround() == null;
            Func<bool> OnContactToWall() => () => AngleOfContact() == 0 && WallSlideDataInstance.HasCooldown == false;
            Func<bool> NoContactToWall() => () => AngleOfContact() == null || AngleOfContact() != 0;
            Func<bool> OnSliceFinished() => () => m_sTeleport.HasFinished;
            Func<bool> AlwaysTrue() => () => true;
        }

        private void Update() => m_StateMachine.Tick();
        private void FixedUpdate() => m_StateMachine.PhysicsTick();

        #endregion

        #region Inputs

        private void OnJumpStarted() => StartCoroutine(RememberJumpRequest(0.15f));

        private void OnAttack1Started() => m_StateMachine.TriggerState(m_sTeleport);
        
        private void OnShiftStarted()
        {
            if(m_StateMachine.TriggerState(m_sShift))
                return;
            
            m_StateMachine.TriggerState(m_sAir);
        }
        
        #endregion

        #region Local Methods
        
        private float? IsTouchingToGround()
        {
            const int rayCount = 8;
            var bounds = m_PlayerCollision.bounds;
            var spaceBetweenRays = bounds.size.x / (rayCount - 1);
            var bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            for (var i = 0; i < 8; i++)
            {
                var hit = Physics2D.Raycast(
                    bottomLeft + Vector2.right * (spaceBetweenRays * i), 
                    Vector2.down, 0.1f, m_Settings.ObstacleLayerMask);

                if (hit)
                    return Vector2.Angle(hit.normal, Vector2.up) % 90f;
            }

            return null;
        }
        
        private RaycastHit2D? IsTouchingToWall()
        {
            if(m_Inputs.m_HorizontalMovement == 0)
                return null;

            var bounds = m_PlayerCollision.bounds;
            const int horizontalRayCount = 2;
            var verticalRaySpace = bounds.size.y / (horizontalRayCount - 1);
            var movementDirection = m_Inputs.m_HorizontalMovement > 0 ? 1 : -1;

            var rayStartPosition = movementDirection == 1 ? new Vector2(bounds.max.x, bounds.min.y) : new Vector2(bounds.min.x, bounds.min.y);

            for (var i = 0; i < 2; i++)
            {
                Debug.DrawRay(rayStartPosition + Vector2.up * verticalRaySpace * i, Vector2.right * movementDirection,
                    Color.red);
                
                
                var hit = Physics2D.Raycast(
                    rayStartPosition + Vector2.up * (verticalRaySpace * i),
                    Vector2.right * movementDirection,
                    0.1f,
                    m_Settings.ObstacleLayerMask);

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
            
            if (m_bHasJumpRequest)
                yield return null;

            m_bHasJumpRequest = true;
            yield return new WaitForSeconds(duration);
            m_bHasJumpRequest = false;
        }

        #endregion
        

    }
    
}
