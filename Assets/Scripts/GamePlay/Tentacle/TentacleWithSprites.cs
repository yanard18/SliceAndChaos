using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenizYanar
{
    public class TentacleWithSprites : MonoBehaviour
    {
        private Vector3[] m_TSegmentVelocities;
        
        [SerializeField]
        private int m_Length;
        
        [SerializeField]
        private LineRenderer m_LineRenderer;
        
        [SerializeField]
        private Vector3[] m_TSegmentPoses;
        
    
        [SerializeField]
        private Transform m_TargetDirection;
        
        [SerializeField]
        private float m_TargetDistance;
        
        [SerializeField]
        private float m_SmoothSpeed;

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

            m_TargetDirection.localRotation =
                Quaternion.Euler(0, 0, Mathf.Sin(Time.time * m_WiggleFrequency) * m_WiggleMagnitude);
            
            m_TSegmentPoses[0] = m_TargetDirection.position;

            for (var i = 1; i < m_TSegmentPoses.Length; i++)
            {
                var targetPos = m_TSegmentPoses[i - 1] + (m_TSegmentPoses[i] - m_TSegmentPoses[i - 1]).normalized * m_TargetDistance;
                m_TSegmentPoses[i] = Vector3.SmoothDamp(m_TSegmentPoses[i], targetPos, ref m_TSegmentVelocities[i], m_SmoothSpeed);
                m_TBodyParts[i - 1].transform.position = m_TSegmentPoses[i];
            }
            
            m_LineRenderer.SetPositions(m_TSegmentPoses);
        }
    }
}
