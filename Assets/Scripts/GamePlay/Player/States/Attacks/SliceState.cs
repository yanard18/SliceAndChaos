using System;
using System.Collections.Generic;
using System.Linq;
using DenizYanar.DamageAndHealthSystem;
using DenizYanar.SenseEngine;
using DenizYanar.FSM;
using DenizYanar.Inputs;
using UnityEngine;

namespace DenizYanar.PlayerSystem.Attacks
{
    public class SliceState : State
    {
        private readonly PlayerAttackController _player;
        private readonly PlayerSettings _settings;


        private static PlayerInputs _input;
        private readonly Action<float> _startAttackCooldown;
        private readonly Rigidbody2D _rb;
        private readonly SenseEnginePlayer _attackSensePlayer;
        private readonly SenseEnginePlayer _hitSensePlayer;


        #region Constructor

        public SliceState
        (
            PlayerAttackController player,
            PlayerSettings settings,
            PlayerInputs input,
            Action<float> startAttackCooldown,
            Rigidbody2D rb,
            SenseEnginePlayer attackSense,
            SenseEnginePlayer hitSense
        )

        {
            
            _player = player;
            _settings = settings;
            _input = input;
            _startAttackCooldown = startAttackCooldown;
            _rb = rb;
            _attackSensePlayer = attackSense;
            _hitSensePlayer = hitSense;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            if (Camera.main is null) return;

            _startAttackCooldown.Invoke(_settings.AttackCooldownDuration);


            var playerPosition = _player.transform.position;

            var attackDir = CalculateAttackDirection(playerPosition);


            var boxDistance = _settings.AttackRadius * Mathf.Sqrt(2);
            var boxAngle = Vector2.SignedAngle(Vector2.right, attackDir) - 45f;
            var boxStartPos = playerPosition + (Vector3) attackDir * (boxDistance / 2f);
            var boxSize = Vector2.one * _settings.AttackRadius;


            Collider2D[] enemiesInRectangleArea =
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics2D.OverlapBoxAll(boxStartPos, boxSize, boxAngle, _settings.EnemyLayerMask);

            Collider2D[] enemiesInCircleArea =
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics2D.OverlapCircleAll(playerPosition, _settings.AttackRadius, _settings.EnemyLayerMask);

            IEnumerable<Collider2D> enemies = enemiesInRectangleArea.Intersect(enemiesInCircleArea);


            PushPlayerAlongAttackDir(attackDir);

            foreach (var enemy in enemies)
            {
                if (IsTargetEqualToPlayer(enemy.transform)) continue;
                if (IsThereWallBetween(playerPosition, enemy.transform.position, _settings.ObstacleLayerMask)) continue;
                if (HasNotHitBox(enemy, out var hitBox)) continue;

                var health = hitBox.m_HealthOfHitBox;
                
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                health.TakeDamage(new Damage(_settings.AttackDamage, _player.gameObject));
                PlayOnHitSense(playerPosition, attackDir);
            }

            _startAttackCooldown.Invoke(_settings.AttackCooldownDuration);
            _attackSensePlayer.Play();


#if UNITY_EDITOR
            DebugDrawBox(boxStartPos, boxSize, boxAngle, Color.blue, 4.0f);
#endif
        }

        

        #endregion

        #region Local Methods

        private static Vector2 CalculateAttackDirection(Vector2 playerPosition)
        {
            if (Camera.main is not null)
            {

                Vector2 attackDir = _input.m_MousePosition - playerPosition; 
                attackDir.Normalize();
                return attackDir;
            }

            return Vector2.zero;
        }

        private void PushPlayerAlongAttackDir(Vector2 attackDir)
        {
            _rb.AddForce(attackDir * _settings.AttackPushForce, ForceMode2D.Impulse);
        }

        private static bool HasNotHitBox(Collider2D enemy, out HitBox hitBox)
        {
            hitBox = enemy.GetComponent<HitBox>();
            return hitBox == null;
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
        
        private void PlayOnHitSense(Vector3 playerPosition, Vector2 attackDir)
        {
            var spawner = _hitSensePlayer.GetComponent<SenseInstantiateObject>();
            spawner.InstantiatePosition = playerPosition + (Vector3) attackDir * -10f;
            var angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
            spawner.InstantiateRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _hitSensePlayer.Play();
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
    }
}