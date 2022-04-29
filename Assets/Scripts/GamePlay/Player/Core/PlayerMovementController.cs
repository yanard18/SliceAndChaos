using System;
using System.Collections;
using DenizYanar.Detection;
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

        private WallDetection m_WallDetection;
        private GroundDetection m_GroundDetection;

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
        private PlayerConfigurations m_Configurations;

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

        public JumpData s_JumpDataInstance { get; private set; }
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
            SetupWallDetection();
            m_GroundDetection = new GroundDetection(m_PlayerCollision, 8, m_Configurations.ObstacleLayerMask);

            SetupStateMachine();
        }

        private void SetupWallDetection()
        {
            m_WallDetection = new WallDetection(m_PlayerCollision, 2, m_Configurations.ObstacleLayerMask);
        }


        private void Update() => m_StateMachine.Tick();


        private void FixedUpdate() => m_StateMachine.PhysicsTick();

        #endregion

        #region Inputs

        private void OnJumpStarted() => StartCoroutine(RememberJumpRequest(0.15f));

        private void OnAttack1Started() => m_StateMachine.TriggerState(m_sTeleport);

        private void OnShiftStarted()
        {
            if (m_StateMachine.TriggerState(m_sShift))
                return;

            m_StateMachine.TriggerState(m_sAir);
        }

        
        #endregion

        #region Local Methods

        private void SetupStateMachine()
        {
            s_JumpDataInstance = new JumpData(2, 20, m_Rb);
            WallSlideDataInstance = new WallSlideData(m_Rb, m_PlayerCollision);
            
            var horizontalPhysicMovement = new HorizontalPhysicMovement(
                m_Rb,
                m_Configurations.AirStrafeMaxXVelocity,
                m_Configurations.AirStrafeXAcceleration
            );


            var verticalPhysicMovement = new VerticalPhysicMovement(
                m_Rb,
                m_Configurations.AirStrafeMaxYVelocity,
                m_Configurations.AirStrafeYAcceleration
            );

            m_StateMachine = new StateMachine();

            m_sIdle = new IdleState(m_Rb, nameInformerEvent: m_ecStateChangeTitle, stateName: "Idle");
            m_sMove = new MoveState(m_Rb, m_Configurations, m_Inputs, nameInformerEvent: m_ecStateChangeTitle,  stateName: "Move");
            m_sJump = new JumpState(this, m_sepJump, nameInformerChannel: m_ecStateChangeTitle, stateName: "Jump");
            m_sLand = new LandState(s_JumpDataInstance, m_sepLand, nameInformerEvent: m_ecStateChangeTitle,    stateName: "Land");
            m_sWallSlide = new WallSlideState(this, m_Configurations, name: m_ecStateChangeTitle, stateName: "Wall Slide");
            m_sAir = new AirState(m_Inputs, horizontalPhysicMovement, verticalPhysicMovement, nameInformerChannel: m_ecStateChangeTitle, stateName: "At Air");
            m_sShift = new ShiftState(m_Rb, m_Inputs, m_Configurations, m_sepEnterShift, m_sepLeaveShift, nameInformerEvent: m_ecStateChangeTitle, stateName: "Shift");
            m_sTeleport = new TeleportState(m_Rb, m_Configurations);

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
            Func<bool> CanJump() => () => m_bHasJumpRequest && s_JumpDataInstance.CanJump;
            Func<bool> WhenJumpKeyTriggered() => () => m_bHasJumpRequest;
            Func<bool> OnFallToGround() => () => IsTouchingToGround() != null && m_Rb.velocity.y <= 0;
            Func<bool> NoMoreContactToGround() => () => IsTouchingToGround() == null;
            Func<bool> OnContactToWall() => () => IsTouchingToWall() && WallSlideDataInstance.HasCooldown == false;
            Func<bool> NoContactToWall() => () => !IsTouchingToWall();
            Func<bool> OnSliceFinished() => () => m_sTeleport.m_bHasFinished;
            Func<bool> AlwaysTrue() => () => true;
        }


        private float? IsTouchingToGround() => m_GroundDetection.IsTouchingToGroundWithAngle();

        private bool IsTouchingToWall() => m_WallDetection.DetectWall(m_Inputs.m_HorizontalMovement * Vector2.right);

        private IEnumerator RememberJumpRequest(float duration)
        {
            if (m_bHasJumpRequest) yield break;
            
            m_bHasJumpRequest = true;
            yield return new WaitForSeconds(duration);
            m_bHasJumpRequest = false;
        }

        #endregion
    }
}