using DenizYanar.YanarPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.Sensors
{
    public class XRayTargetSensor : MonoBehaviour, ISensor
    {
        [SerializeField]
        [Range(0.1f, 100f)]
        private float m_Range;

        [SerializeField]
        [Required]
        private LayerMask m_TargetLayer;

        [SerializeField]
        private Color m_GizmoColor = Color.yellow;

        public Transform Scan()
        {
            Collider2D[] TTargets = new Collider2D[10];
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, m_Range, TTargets, m_TargetLayer);
            if (size == 0) return null;

            // var target = FindClosestTarget(TTargets);
            Vector2 selfPosition = transform.position;
            var target = YanarUtils.FindClosestTarget(selfPosition, TTargets);
            var rootTransformOfTarget = target.transform.root;
            return rootTransformOfTarget;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = m_GizmoColor;
            Gizmos.DrawWireSphere(transform.position, m_Range);
        }
    }
}