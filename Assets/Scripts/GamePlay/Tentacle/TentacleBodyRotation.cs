using UnityEngine;

namespace DenizYanar
{
    public class TentacleBodyRotation : MonoBehaviour
    {
        public Transform m_FollowTarget;
        public float m_RotationSpeed;

        private void Update()
        {
            var dir = m_FollowTarget.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var desiredRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, m_RotationSpeed * Time.deltaTime);
        }
    }
}
