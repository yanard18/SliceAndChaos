using DenizYanar.Projectiles;
using UnityEngine;

namespace DenizYanar.PlayerSystem
{
    [CreateAssetMenu(menuName = "Slice And Chaos/Player/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Movement Configurations")]
        
        [SerializeField] private float _desiredMovementVelocity = 17.0f;
        public float DesiredMovementVelocity => _desiredMovementVelocity;
        
        [SerializeField] private float _movementAcceleration = 80.0f;
        public float MovementAcceleration => _movementAcceleration;

        [Header("Jump Configurations")]
        
        [SerializeField] private float _jumpDelayDuration = 0.1f;
        public float JumpDelayDuration => _jumpDelayDuration;

        [SerializeField] private float _jumpForce = 10.0f;
        public float JumpForce => _jumpForce;

        [SerializeField] private int _jumpCount;
        public int JumpCount => _jumpCount;

        [Header("Wall Slide Settings")] 
        [SerializeField] private float _wallSlideGravity = 0.5f;
        public float WallSlideGravity => _wallSlideGravity;

        [SerializeField] private float _wallSlideHorizontalBouncePower = 17.0f;
        public float WallSlideHorizontalBouncePower => _wallSlideHorizontalBouncePower;

        [SerializeField] private float _wallSlideVerticalBouncePower = 20.0f;
        public float WallSlideVerticalBouncePower => _wallSlideVerticalBouncePower;

        [Header("Air Strafe Configurations")] 
        
        [SerializeField] private float _airStrafeXAcceleration = 100.0f;
        public float AirStrafeXAcceleration => _airStrafeXAcceleration;

        [SerializeField] private float _airStrafeMaxXVelocity = 20.0f;
        public float AirStrafeMaxXVelocity => _airStrafeMaxXVelocity;

        [SerializeField] private float _airStrafeYAcceleration = -200.0f;
        public float AirStrafeYAcceleration => _airStrafeYAcceleration;

        [SerializeField] private float _airStrafeMaxYVelocity = 25.0f;
        public float AirStrafeMaxYVelocity => _airStrafeMaxYVelocity;
        
        
        [Header("Shift Mode Configurations")] 
        
        [SerializeField] private float _shiftModeSpeed;
        public float ShiftModeSpeed => _shiftModeSpeed;

        [SerializeField] private float _shiftModeTurnSpeed;
        public float ShiftModeTurnSpeed => _shiftModeTurnSpeed;

        [Header("Slice Teleport Settings")] 
        
        [SerializeField] private float _sliceTeleportDistance = 15.0f;
        public float SliceTeleportDistance => _sliceTeleportDistance;

        [SerializeField] private float _sliceSpeedReductionAfterTeleport = 2.0f;
        public float SliceSpeedReductionAfterTeleport => _sliceSpeedReductionAfterTeleport;

        [Header("Magnet Settings")] 
        
        [SerializeField] private float _magnetPullStrength = 500.0f;
        public float MagnetPullStrength => _magnetPullStrength;

        [SerializeField] private float _magnetPushStrength = 1500.0f;
        public float MagnetPushStrength => _magnetPushStrength;
        
        [SerializeField] private float _magnetAffectRadius = 15.0f;
        public float MagnetAffectRadius => _magnetAffectRadius;
        
        [SerializeField] private float _magnetPullDistanceScale = 1.0f;
        public float MagnetPullDistanceScale => _magnetPullDistanceScale;

        [SerializeField] private float _magnetPushDistanceScale = 1.0f;
        public float MagnetPushDistanceScale => _magnetPushDistanceScale;
        
        
        [Header("Sword Throw Settings")] 
        
        [SerializeField] private Projectile _swordProjectile;

        public Projectile SwordProjectile => _swordProjectile;
        
        [SerializeField] private float _swordThrowSpeed = 50.0f;
        public float SwordThrowSpeed => _swordThrowSpeed;

        [SerializeField] private float _swordThrowAngularVelocity = 2000.0f;
        public float SwordThrowAngularVelocity => _swordThrowAngularVelocity;
        
        

        [Header("Layers")] 
        
        [SerializeField] private LayerMask _obstacleLayerMask;
        public LayerMask ObstacleLayerMask => _obstacleLayerMask;


    }
}
