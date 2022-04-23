using UnityEngine;

namespace DenizYanar.Tentacles
{
    public class Tentacle : MonoBehaviour
    {
        private Vector3[] m_TSegmentVelocities;
        
        
        [SerializeField]
        private int m_Length;
        
        [SerializeField]
        private LineRenderer m_LineRenderer;
        
        [SerializeField]
        private Vector3[] m_TSegmentPoses;
        
        [SerializeField]
        private Transform m_TargetDir;
        
        [SerializeField]
        private float m_TargetDistance;
        
        [SerializeField]
        private float m_SmoothSpeed;
        
        [SerializeField]
        private float m_TrailSpeed;

        [SerializeField]
        private float m_WiggleFrequency;
        
        [SerializeField]
        private float m_WiggleMagnitude;
        
        [SerializeField]
        private GameObject[] m_TBodyParts;
        


        private void Start()
        {
            m_LineRenderer.positionCount = m_Length;
            m_TSegmentPoses = new Vector3[m_Length];
            m_TSegmentVelocities = new Vector3[m_Length];
        }

        private void Update()
        {

            m_TargetDir.localRotation =
                Quaternion.Euler(0, 0, Mathf.Sin(Time.time * m_WiggleFrequency) * m_WiggleMagnitude);
            
            m_TSegmentPoses[0] = m_TargetDir.position;

            for (var i = 1; i < m_TSegmentPoses.Length; i++)
            {
                m_TSegmentPoses[i] = Vector3.SmoothDamp(m_TSegmentPoses[i],
                    m_TSegmentPoses[i - 1] + m_TargetDir.right * m_TargetDistance, ref m_TSegmentVelocities[i], m_SmoothSpeed + i / m_TrailSpeed);
                m_TBodyParts[i - 1].transform.position = m_TSegmentPoses[i];
            }
            
            m_LineRenderer.SetPositions(m_TSegmentPoses);
        }
    }
}
