using System.Collections.Generic;
using System.Linq;
using DenizYanar.Inputs;
using DenizYanar.PlayerSystem;
using DenizYanar.PlayerSystem.Attacks;
using DenizYanar.YanarPro;
using UnityEngine;

namespace DenizYanar.DamageAndHealthSystem
{
    public class VisionDamageArea : IDamageArea
    {
        private readonly PlayerAttackController m_PlayerAttackController;
        private readonly PlayerConfigurations m_Configurations;
        private readonly PlayerInputs m_PlayerInputs;

        public VisionDamageArea(PlayerInputs playerInputs, PlayerConfigurations configurations, PlayerAttackController attackController)
        {
            m_PlayerInputs = playerInputs;
            m_Configurations = configurations;
            m_PlayerAttackController = attackController;
        }


        public void CreateArea(Damage damage)
        {
            // Check is camera exist
            if (Camera.main is null) return;


            // Calculate attack direction
            Vector2 playerPosition = m_PlayerAttackController.transform.position;

            var attackDir =
                YanarUtils.FindDirectionBetweenPositionAndMouse(playerPosition, m_PlayerInputs.m_MousePosition);

#if UNITY_EDITOR
            Debug.DrawRay(playerPosition, attackDir, Color.green, 5.0f);
#endif

            // Find enemies inside of an area
            var boxDistance = m_Configurations.AttackRadius * Mathf.Sqrt(2);
            var boxAngle = Vector2.SignedAngle(Vector2.right, attackDir) - 45f;
            var boxStartPos = playerPosition + attackDir * (boxDistance / 2f);
            var boxSize = Vector2.one * m_Configurations.AttackRadius;


            Collider2D[] targetsInRectangleArea =
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics2D.OverlapBoxAll(boxStartPos, boxSize, boxAngle, m_Configurations.m_HitBoxLayer);

            Collider2D[] targetsInCircleArea =
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics2D.OverlapCircleAll(playerPosition, m_Configurations.AttackRadius,
                    m_Configurations.m_HitBoxLayer);

            IEnumerable<Collider2D> targets = targetsInRectangleArea.Intersect(targetsInCircleArea);
            
#if UNITY_EDITOR
            YanarGizmos.DebugDrawBox(boxStartPos, boxSize, boxAngle, Color.red, 4.0f);
#endif

            // Give damage enemies that inside of attack area
            foreach (var target in targets)
            {
                if (IsTargetEqualToPlayer(target.transform)) continue;
                if (IsThereWallBetween(playerPosition, target.transform.position,
                    m_Configurations.ObstacleLayerMask)) continue;
                if (target.GetComponent<IDamage>() == null) continue;

                Debug.Log("Give Damage");
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                target.GetComponent<IDamage>()
                    .TakeDamage(new Damage(m_Configurations.AttackDamage, m_PlayerAttackController.gameObject));
                
                //PlayOnHitSense(playerPosition, attackDir);
            }
        }
        
         private bool IsTargetEqualToPlayer(Transform enemy)
                {
                    return enemy.root == m_PlayerAttackController.transform.root;
                }
        
                private static bool IsThereWallBetween(Vector2 startPos, Vector2 endPos, LayerMask layerMask)
                {
                    var dir = endPos - startPos;
                    var hit = Physics2D.Raycast(startPos, dir.normalized, dir.magnitude, layerMask);
                    return hit;
                }
    }
}