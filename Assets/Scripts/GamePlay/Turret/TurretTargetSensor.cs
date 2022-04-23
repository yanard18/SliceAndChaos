using UnityEngine;

namespace DenizYanar.Turret
{
    public class TurretTargetSensor : MonoBehaviour
    {
        [SerializeField]
        private LayerMask m_TargetLayer;
        
        [SerializeField]
        private LayerMask m_ObstacleLayer;
        
        [SerializeField]
        private float m_range = 20.0f;
        
        [SerializeField]
        private float m_AngleLimit = 60.0f;
        
        public Transform Detect()
        {
            var target = Physics2D.OverlapCircle(transform.position, m_range, m_TargetLayer);

            if (target is null)
                return null;

            var turretPosition = transform.position;
            var dir = target.transform.position - turretPosition;

            var isThereAnyObstacleBetweenRotorAndTarget = Physics2D.Raycast
                (
                turretPosition, 
                dir.normalized, 
                dir.magnitude, 
                m_ObstacleLayer
                );
            
            
            if (isThereAnyObstacleBetweenRotorAndTarget) return null;
            
            var angle = Vector2.Angle(dir, transform.right);
            

            return Mathf.Abs(angle) <= m_AngleLimit ? target.transform : null;
        }
        
        
        
    }
}
