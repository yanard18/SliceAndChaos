using System.Collections;
using DenizYanar.Events;
using DenizYanar.FSM;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerWallSlideState : State
    {
        private const float _wallSlideGravityScale = 0.5f;
        
        private readonly Rigidbody2D _rb;
        private readonly PlayerMovementController _playerMovementController;
        private readonly float _defaultGravityScale;
        private readonly Collider2D _collider;
        private readonly PlayerSettings _settings;

        public PlayerWallSlideState(PlayerMovementController playerMovementController, PlayerSettings settings, StringEventChannelSO nameInformerEventChannel = null, [CanBeNull] string stateName = null)
        {
            _playerMovementController = playerMovementController;
            _rb = playerMovementController.WallSlideDataInstance.RB;
            _collider = playerMovementController.WallSlideDataInstance.Collider;
            _settings = settings;
            
            
            _stateNameInformerEventChannel = nameInformerEventChannel;
            _stateName = stateName ?? GetType().Name;


            _defaultGravityScale = _rb.gravityScale;
            

        }

        public override void OnEnter()
        {
            base.OnEnter();
            _rb.gravityScale = _wallSlideGravityScale;
            _rb.velocity /= 4.0f;
            _playerMovementController.JumpDataInstance.ResetJumpCount();
        }

        public override void OnExit()
        {
            base.OnExit();

            _rb.gravityScale = _defaultGravityScale;


            Vector2? hitNormal = FindWallContactNormal();

            if (hitNormal == null) 
                return;
            
            ExecuteJump(hitNormal);
        }

        private void ExecuteJump(Vector2? hitNormal)
        {
            _rb.velocity = (Vector2) (hitNormal * 17 + Vector2.up * 20);
            _playerMovementController.StartCoroutine(_playerMovementController.WallSlideDataInstance.StartCooldown(0.12f));
        }

        private Vector2? FindWallContactNormal()
        {
            const int horizontalRayCount = 2;
            Bounds bounds = _collider.bounds;
            var horizontalRaySpacing = bounds.size.y;


            Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);

            for (var i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayDirection = Vector2.left;
                RaycastHit2D hit = Physics2D.Raycast(bottomLeft, rayDirection, 0.1f, _settings.ObstacleLayerMask);
                Debug.DrawRay(bottomLeft, Vector2.left * 5, Color.red, 3);
                
                if(hit)
                    return hit.normal;
            }

            for (var i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayStartPos = bottomRight + Vector2.up * (i * horizontalRaySpacing);
                Vector2 rayDirection = Vector2.right;
                
                RaycastHit2D hit = Physics2D.Raycast(rayStartPos, rayDirection, 0.1f, _settings.ObstacleLayerMask);
                Debug.DrawRay(rayStartPos, Vector2.right * 5, Color.red, 3);
                
                if(hit)
                    return hit.normal;
            }

            return null;
        }
        

    }
    
    public class WallSlideData
    {
        public bool HasCooldown;
        public readonly Rigidbody2D RB;
        public readonly Collider2D Collider;

        public WallSlideData(Rigidbody2D rb, Collider2D collider)
        {
            RB = rb;
            Collider = collider;
        }
        
        public IEnumerator StartCooldown(float duration)
        {
            HasCooldown = true;
            yield return new WaitForSeconds(duration);
            HasCooldown = false;
        }
    }
}
