using DenizYanar.Detection;
using DenizYanar.YanarPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.Sensors
{
    public class TargetSensor : Sensor
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

        public override Transform Scan()
        {
            Collider2D[] TTargets = new Collider2D[10];
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, m_Range, TTargets, m_TargetLayer);
            if (size == 0) return null;
            
            // var target = FindClosestTarget(TTargets);
            Vector2 selfPosition = transform.position;
            var target = YanarUtils.FindClosestTarget(selfPosition, TTargets);
            var rootTransformOfTarget = target.transform.root;
            var bIsThereWallBetween = m_WallDetection.IsThereWallBetween(selfPosition,rootTransformOfTarget.position);
            return bIsThereWallBetween ? null : rootTransformOfTarget;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = m_GizmoColor;
            Gizmos.DrawWireSphere(transform.position, m_Range);
        }
    }
}