using System;
using System.Collections.Generic;
using System.Linq;
using DenizYanar.Core;
using DenizYanar.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar.PlayerSystem
{
    public class PlayerAttackSlashState : State
    {
        private readonly PlayerAttackController _player;
        private readonly PlayerSettings _settings;
        private readonly Action<float> _startAttackCooldown;


        #region Constructor

        public PlayerAttackSlashState(PlayerAttackController player, PlayerSettings settings,
            Action<float> startAttackCooldown)
        {
            _player = player;
            _settings = settings;
            _startAttackCooldown = startAttackCooldown;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            if (Camera.main is null) return;

            _startAttackCooldown.Invoke(_settings.AttackCooldownDuration);


            var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var playerPosition = _player.transform.position;
            Vector2 attackDir = mousePos - playerPosition;
            attackDir.Normalize();


            var boxDistance = _settings.AttackRadius * Mathf.Sqrt(2);
            var boxAngle = Vector2.SignedAngle(Vector2.right, attackDir) - 45f;
            var boxStartPos = playerPosition + (Vector3) attackDir * (boxDistance / 2f);
            var boxSize = Vector2.one * _settings.AttackRadius;


            Collider2D[] enemiesInRectangleArea =
                Physics2D.OverlapBoxAll(boxStartPos, boxSize, boxAngle, _settings.EnemyLayerMask);

            Collider2D[] enemiesInCircleArea =
                Physics2D.OverlapCircleAll(playerPosition, _settings.AttackRadius, _settings.EnemyLayerMask);

            IEnumerable<Collider2D> enemies = enemiesInRectangleArea.Intersect(enemiesInCircleArea);

            foreach (var enemy in enemies)
            {
                Debug.Log(enemy.transform.root.name);
                if (IsTargetEqualToPlayer(enemy.transform)) continue;
                if (IsThereWallBetween(playerPosition, enemy.transform.position, _settings.ObstacleLayerMask)) continue;
                if (HasNotHealth(enemy, out var health)) continue;

                Debug.Log("DAMAGE!");
                health.TakeDamage(new Damage(_settings.AttackDamage, _player.gameObject));
            }


#if UNITY_EDITOR
            DebugDrawBox(boxStartPos, boxSize, boxAngle, Color.blue, 4.0f);
#endif
        }

        private static bool HasNotHealth(Collider2D enemy, out Health health)
        {
            health = enemy.transform.root.GetComponent<Health>();
            return health == null;
        }


        private bool IsTargetEqualToPlayer(Transform enemy)
        {
            return enemy.root == _player.transform.root;
        }

        private static bool IsThereWallBetween(Vector2 startPos, Vector2 endPos, LayerMask layerMask)
        {
            var dir = endPos - startPos;
            var hit = Physics2D.Raycast(startPos, dir.normalized, dir.magnitude, layerMask);
            return hit;
        }

        private static void DebugDrawBox(Vector2 point, Vector2 size, float angle, Color color, float duration)
        {
            var orientation = Quaternion.Euler(0, 0, angle);

            // Basis vectors, half the size in each direction from the center.
            Vector2 right = orientation * Vector2.right * size.x / 2f;
            Vector2 up = orientation * Vector2.up * size.y / 2f;

            // Four box corners.
            var topLeft = point + up - right;
            var topRight = point + up + right;
            var bottomRight = point - up + right;
            var bottomLeft = point - up - right;

            // Now we've reduced the problem to drawing lines.
            Debug.DrawLine(topLeft, topRight, color, duration);
            Debug.DrawLine(topRight, bottomRight, color, duration);
            Debug.DrawLine(bottomRight, bottomLeft, color, duration);
            Debug.DrawLine(bottomLeft, topLeft, color, duration);
        }

        #endregion

        #region Local Methods

        #endregion
    }
}