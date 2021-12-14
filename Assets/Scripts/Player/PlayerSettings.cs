using UnityEngine;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "SliceAndChaos/Player/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        
        [Header("Movement Configurations")]
        
        [SerializeField] private float _horizontalMovementSpeed = 10.0f;
        public float HorizontalMovementSpeed => _horizontalMovementSpeed;

        [Header("Jump Configurations")]
        
        [SerializeField] private float _jumpDelayDuration = 0.1f;
        public float JumpDelayDuration => _jumpDelayDuration;

        [SerializeField] private float _jumpForce = 10.0f;
        public float JumpForce => _jumpForce;

        [SerializeField] private int _jumpCount;
        public int JumpCount => _jumpCount;


        [Header("Layers")] 
        
        [SerializeField] private LayerMask _obstacleLayerMask;
        public LayerMask ObstacleLayerMask => _obstacleLayerMask;


    }
}
