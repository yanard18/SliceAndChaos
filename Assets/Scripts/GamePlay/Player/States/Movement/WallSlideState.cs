using System.Collections;
using DenizYanar.Events;
using DenizYanar.FSM;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar.PlayerSystem.Movement
{
    public class WallSlideState : State
    {
        private readonly Rigidbody2D _rb;
        private readonly PlayerMovementController _playerMovementController;
        private readonly Collider2D _collider;
        private readonly PlayerConfigurations m_Configurations;
        private readonly float _defaultGravityScale;


        #region Constructor

        public WallSlideState(PlayerMovementController playerMovementController, PlayerConfigurations configurations, StringEvent name = null, [CanBeNull] string stateName = null)
        {
            _playerMovementController = playerMovementController;
            _rb = playerMovementController.WallSlideDataInstance.Rb;
            _collider = playerMovementController.WallSlideDataInstance.Collider;
            m_Configurations = configurations;
            
            m_ecStateName = name;
            m_StateName = stateName ?? GetType().Name;
            
            _defaultGravityScale = _rb.gravityScale;
        }

        #endregion

        #region Callback States

        public override void OnEnter()
        {
            base.OnEnter();
            _rb.gravityScale = m_Configurations.WallSlideGravity;
            _rb.velocity /= 4.0f;
            _playerMovementController.s_JumpDataInstance.ResetJumpCount();
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

        #endregion
        
        #region Local Methods

        private void ExecuteJump(Vector2? hitNormal)
        {

            if (hitNormal != null)
            {
                var horizontal = m_Configurations.WallSlideHorizontalBouncePower;
                var vertical = m_Configurations.WallSlideVerticalBouncePower;
                _rb.velocity = (hitNormal.Value * horizontal) + (Vector2.up * vertical);
            }
            
            _playerMovementController.StartCoroutine(_playerMovementController.WallSlideDataInstance.StartCooldown(0.12f));
        }

        private Vector2? FindWallContactNormal()
        {
            const int horizontalRayCount = 2;
            var bounds = _collider.bounds;
            var horizontalRaySpacing = bounds.size.y;


            var bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            var bottomRight = new Vector2(bounds.max.x, bounds.min.y);

            for (var i = 0; i < horizontalRayCount; i++)
            {
                var rayDirection = Vector2.left;
                var hit = Physics2D.Raycast(bottomLeft, rayDirection, 0.1f, m_Configurations.ObstacleLayerMask);
                Debug.DrawRay(bottomLeft, Vector2.left * 5, Color.red, 3);
                
                if(hit)
                    return hit.normal;
            }

            for (var i = 0; i < horizontalRayCount; i++)
            {
                var rayStartPos = bottomRight + Vector2.up * (i * horizontalRaySpacing);
                var rayDirection = Vector2.right;
                
                var hit = Physics2D.Raycast(rayStartPos, rayDirection, 0.1f, m_Configurations.ObstacleLayerMask);
                Debug.DrawRay(rayStartPos, Vector2.right * 5, Color.red, 3);
                
                if(hit)
                    return hit.normal;
            }

            return null;
        }

        #endregion
       
    }
    
    public class WallSlideData
    {
        public bool HasCooldown;
        public readonly Rigidbody2D Rb;
        public readonly Collider2D Collider;

        public WallSlideData(Rigidbody2D rb, Collider2D collider)
        {
            Rb = rb;
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
