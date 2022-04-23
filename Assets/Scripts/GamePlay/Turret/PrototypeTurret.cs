using UnityEngine;

namespace DenizYanar
{
    public class PrototypeTurret : MonoBehaviour
    {
        [SerializeField]
        private LayerMask m_PlayerLayer;
        
        private TurretGunInputReader m_Input;

        private void Awake() => m_Input = GetComponentInChildren<TurretGunInputReader>();
        private void Update()
        {
            var player = Physics2D.OverlapCircle(transform.position, 15.0f, m_PlayerLayer);
            if (player is null)
            {
                m_Input.InvokeOnFireCancelled();
                return;
            }

            var dir = player.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.Rotate(Vector3.forward * 90);
            m_Input.InvokeOnFireStarted();
        }
    }
}
