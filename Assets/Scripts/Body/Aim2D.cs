using DenizYanar.YanarPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar
{
    public class Aim2D : MonoBehaviour
    {
        public bool m_bActivated;
        
        [SerializeField]
        private float m_fAngleConstraint = 60.0f;
        
        [SerializeField] 
        private bool m_bMoveToTarget;

        
        [SerializeField] [ShowIf("@m_bMoveToTarget")]
        private float m_fMoveScale = 1.0f;


        private Vector2 m_oStartPosition;

        private void Start()
        {
            m_oStartPosition = transform.localPosition;
        }

        

        private void Update()
        {
            if(!m_bActivated) return;
            
            if (Camera.main is not null) Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        private void Aim(Vector2 tPos)
        {
            var oPos = transform.position;
            
            var tDir = YanarUtils.FindDirectionBetweenTwoPositions(oPos, tPos);
            var fAngle = YanarUtils.FindAngleBetweenTwoPositions(oPos, tPos);
            var bIsTargetBehind = Vector2.Dot(oPos, tPos) < 0;
            
            if(bIsTargetBehind) Turn();
            
            if(Mathf.Abs(fAngle) > m_fAngleConstraint) return;

            transform.localRotation = Quaternion.AngleAxis(fAngle, Vector3.forward);

            if (m_bMoveToTarget)
                transform.localPosition = m_oStartPosition + tDir.normalized * m_fMoveScale;

        }

        private void Turn()
        {
            Debug.Log("Turned!");
        }
    }
}
