using System;
using System.Collections.Generic;
using DenizYanar.Detection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.Sensors
{
    public class TargetSensor : MonoBehaviour, ISensor
    {
        private WallDetectionBetweenPoints m_WallDetection;
        
        [SerializeField]
        [Range(0.1f, 100f)]
        private float m_Range;

        [SerializeField]
        [Required]
        private LayerMask m_TargetLayer;

        [SerializeField]
        [Required]
        private LayerMask m_ObstacleLayer;

        [SerializeField]
        private Color m_GizmoColor = Color.yellow;

        private void Awake()
        {
            m_WallDetection = new WallDetectionBetweenPoints(m_ObstacleLayer);
        }

        public Vector2? Scan()
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            Collider2D[] TTargets = Physics2D.OverlapCircleAll(transform.position, m_Range, m_TargetLayer);
            if (TTargets.Length == 0) return null;
            
            // var target = FindClosestTarget(TTargets);
            var target = TTargets[0];
            var rootTransformOfTarget = target.transform.root;
            var bIsThereWallBetween = m_WallDetection.IsThereWallBetween(transform.position,rootTransformOfTarget.position);
            if (bIsThereWallBetween) return null;
            return rootTransformOfTarget.position;
        }

        private Collider2D FindClosestTarget(Collider2D[] TTargets)
        {
            Debug.Log(TTargets.Length);
            var closestDistance = 0f;
            var closestTarget = new Collider2D();
            foreach (var target in TTargets)
            {
                var distance = Vector2.Distance(transform.position, target.transform.root.position);
                if (!(distance < closestDistance)) continue;
                
                closestDistance = distance;
                closestTarget = target;

            }

            return closestTarget;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = m_GizmoColor;
            Gizmos.DrawWireSphere(transform.position, m_Range);
        }
    }
}