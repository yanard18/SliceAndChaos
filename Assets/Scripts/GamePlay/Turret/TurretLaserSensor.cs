using System;
using UnityEngine;

namespace DenizYanar.Turret
{
    
    [RequireComponent(typeof(LineRenderer))]
    public class TurretLaserSensor : MonoBehaviour
    {
        private LineRenderer m_LR;

        [SerializeField]
        private float m_Range = 20.0f;

        [SerializeField] 
        private LayerMask m_ObstacleLayer;
        
        [SerializeField]
        private LayerMask m_TargetLayer;


        public bool IsDetected;

        private void Awake() => m_LR = GetComponent<LineRenderer>();

        private void Update()
        {
            var localTransform = transform;
            Vector2 pos = localTransform.position;
            
            SetLineRenderer(pos, localTransform);
        }

        public bool HandleDetection()
        {
            var selfTransform = transform;
            var hitTarget = Physics2D.Raycast(selfTransform.position, selfTransform.right, m_Range, m_TargetLayer);
            return hitTarget;
        }

        private void SetLineRenderer(Vector2 pos, Transform localTransform)
        {
            var hit = Physics2D.Raycast(pos, localTransform.right, m_Range, m_ObstacleLayer);

            m_LR.SetPosition(0, pos);

            if (hit)
                m_LR.SetPosition(1, hit.point);
            else
                m_LR.SetPosition(1, (Vector2) (localTransform.right * 20) + pos);
        }
        
    }
}
