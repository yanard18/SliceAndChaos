using UnityEngine;

namespace DenizYanar.Turret
{
    public class PlayerSensor : MonoBehaviour
    {
        [SerializeField]
        private float m_Range = 20.0f;
        
        [SerializeField]
        private LayerMask m_PlayerLayer;

        
        
        public Vector2 FindTarget()
        {
            var p = Physics2D.OverlapCircle(transform.position, m_Range, m_PlayerLayer);
            return p.transform.position;
        }
    }
}
