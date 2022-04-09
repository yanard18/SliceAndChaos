using UnityEngine;

namespace DenizYanar.Turret
{
    public class TurretTargetSensor : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private LayerMask _obstacleLayerMask;
        [SerializeField] private float _range = 20.0f;
        [SerializeField] private float _angle = 60.0f;
        
        public Transform Detect()
        {
            var target = Physics2D.OverlapCircle(transform.position, _range, _targetLayerMask);

            if (target is null)
                return null;

            var turretPosition = transform.position;
            var dir = target.transform.position - turretPosition;

            var isThereAnyObstacleBetweenRotorAndTarget = Physics2D.Raycast
                (
                turretPosition, 
                dir.normalized, 
                dir.magnitude, 
                _obstacleLayerMask
                );
            
            
            if (isThereAnyObstacleBetweenRotorAndTarget) return null;
            
            var angle = Vector2.Angle(dir, transform.right);
            

            return Mathf.Abs(angle) <= _angle ? target.transform : null;
        }
        
        
        
    }
}
