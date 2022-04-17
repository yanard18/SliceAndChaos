using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Enemies/Crawler Settings", fileName = "Crawler")]
    public class CrawlerSettings : EnemySettings
    {
        [Title("Crawler Special Settings")]
        public float JumpCooldown;
        
        [HorizontalGroup("Crawler Stats")]
        public float VerticalJumpForce;
        
        [HorizontalGroup("Crawler Stats")]
        public float HorizontalJumpForce;
    }
}
