using UnityEngine;

namespace DenizYanar.Detection
{
    public class WallDetectionBetweenPoints
    {

        private readonly LayerMask m_ObstacleLayer;

        public WallDetectionBetweenPoints(LayerMask obstacleLayer)
        {
            m_ObstacleLayer = obstacleLayer;
        }
        
        public bool IsThereWallBetween(Vector2 startPos, Vector2 endPos)
        {
            var dir = endPos - startPos;
            var hit = Physics2D.Raycast(startPos, dir.normalized, dir.magnitude, m_ObstacleLayer);
            return hit;
        }
    }
}