using UnityEngine;

namespace DenizYanar
{
    public class TentacleBodyRotation : MonoBehaviour
    {
        public Transform FollowTarget;
        public float RotationSpeed;

        private void Update()
        {
            var dir = FollowTarget.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var desiredRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, RotationSpeed * Time.deltaTime);
        }
    }
}
