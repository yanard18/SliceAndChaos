using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    [CreateAssetMenu(menuName = "Enemies/Crawler Settings", fileName = "Crawler")]
    public class CrawlerSettings : EnemySettings
    {
        [Title("Crawler Special Settings")]
        public float m_JumpCooldown;
        
        [HorizontalGroup("Crawler Stats")]
        public float m_VerticalJumpForce;
        
        [HorizontalGroup("Crawler Stats")]
        public float m_HorizontalJumpForce;
    }
}
