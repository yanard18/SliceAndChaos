using UnityEngine;

namespace DenizYanar.Detection
{
    public class GroundDetection
    {
        private readonly int m_RayCount;
        private readonly Collider2D m_Collider;
        private readonly LayerMask m_ObstacleLayer;

        public GroundDetection(Collider2D collider, int rayCount, LayerMask obstacleLayer)
        {
            m_Collider = collider;
            m_RayCount = rayCount;
            m_ObstacleLayer = obstacleLayer;
        }

        public bool IsTouchingToGround()
        {
            var raySpacing = CalculateRaySpacing(m_Collider.bounds, m_RayCount);
            return CreateRays(m_RayCount, raySpacing);
        }

        public float? IsTouchingToGroundWithAngle()
        {
            var raySpacing = CalculateRaySpacing(m_Collider.bounds, m_RayCount);
            var hit = CreateRays(m_RayCount, raySpacing);
            if (hit)
                return Vector2.Angle(hit.normal, Vector2.up) % 90f;

            return null;
        }

        private float CalculateRaySpacing(Bounds bounds, int rayCount)
        {
            if (rayCount <= 1) rayCount = 2;
            return bounds.size.x / (rayCount - 1);
        }

        private RaycastHit2D CreateRays(int rayCount, float raySpacing)
        {
            var hit = new RaycastHit2D();
            for (var i = 0; i < rayCount; i++)
            {
                var startPos = BottomLeftCorner + Vector2.right * (raySpacing * i);
                hit = Physics2D.Raycast(startPos, Vector2.down, 0.1f, m_ObstacleLayer);
                if (hit) return hit;
            }

            return hit;
        }

        private Vector2 BottomLeftCorner => new Vector2(m_Collider.bounds.min.x, m_Collider.bounds.min.y);
    }
}