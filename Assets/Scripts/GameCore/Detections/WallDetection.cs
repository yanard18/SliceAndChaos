using System;
using UnityEngine;

namespace DenizYanar.Detection
{
    public class WallDetection
    {
        private readonly Collider2D m_Collider;
        private readonly int m_RayCount;
        private readonly LayerMask m_ObstacleLayer;

        public WallDetection(Collider2D collider, int rayCount, LayerMask obstacleLayer)
        {
            m_Collider = collider;
            m_RayCount = rayCount;
            m_ObstacleLayer = obstacleLayer;
        }

        public bool DetectWall(Vector2 movementVelocity, float predictRange = 0.1f)
        {
            if (movementVelocity.x == 0) return false;
            
            var bounds = m_Collider.bounds;
            const int horizontalRayCount = 2;
            var verticalRaySpace = bounds.size.y / (horizontalRayCount - 1);
            var movementDirection = movementVelocity.x > 0 ? 1 : -1;

            var rayStartPosition = movementDirection == 1
                ? new Vector2(bounds.max.x, bounds.min.y)
                : new Vector2(bounds.min.x, bounds.min.y);

            
            
            for (var i = 0; i < m_RayCount; i++)
            {
                Debug.DrawRay(rayStartPosition + Vector2.up * verticalRaySpace * i, Vector2.right * movementDirection,
                    Color.red);


                var hit = Physics2D.Raycast(
                    rayStartPosition + Vector2.up * (verticalRaySpace * i),
                    Vector2.right * movementDirection,
                    predictRange,
                    m_ObstacleLayer
                );

                if (!hit && Math.Abs(CalculateAngelOfHitNormal(hit) - 90) > 2)
                    return false;
            }

            return true;
        }

        private static float CalculateAngelOfHitNormal(RaycastHit2D hit)
        {
            var angle = Vector2.Angle(hit.normal, Vector2.up);
            angle %= 90;
            return angle;
        }
    }
}