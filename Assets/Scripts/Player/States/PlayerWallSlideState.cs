using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerWallSlideState : State
    {
        private readonly Rigidbody2D _rb;
        private readonly Player _player;
        private const float _wallSlideGravityScale = 0.5f;
        private readonly float _defaultGravityScale;
        private readonly Collider2D _collider;
        private readonly PlayerSettings _settings;
        private RaycastHit2D? _hit;
        
        public PlayerWallSlideState(Player player, PlayerSettings settings, StringEventChannelSO nameInformerEventChannel = null, [CanBeNull] string stateName = null)
        {
            _player = player;
            _rb = player.WallSlideDataInstance.RB;
            _collider = player.WallSlideDataInstance.Collider;
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
            _player.JumpDataInstance.ResetJumpCount();
        }

        public override void OnExit()
        {
            base.OnExit();

            _rb.gravityScale = _defaultGravityScale;


            Vector2? hitNormal = FindWallContactNormal();

            if (hitNormal != null)
            {
                _rb.velocity = (Vector2) (hitNormal * 17 + Vector2.up * 20);
                _player.StartCoroutine(_player.WallSlideDataInstance.StartCooldown(0.12f));
            }
        }

        private Vector2? FindWallContactNormal()
        {
            const int horizontalRayCount = 2;
            var horizontalRaySpacing = _collider.bounds.size.y;


            Vector2 bottomLeft = new Vector2(_collider.bounds.min.x, _collider.bounds.min.y);
            Vector2 bottomRight = new Vector2(_collider.bounds.max.x, _collider.bounds.min.y);

            for (var i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayStartPos = bottomLeft + Vector2.up * (i * horizontalRaySpacing);
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
}
