using UnityEngine;
using DenizYanar.Projectiles;
using Sirenix.OdinInspector;

namespace DenizYanar.Guns
{
    public class GunLauncher : MonoBehaviour
    {
        [SerializeField] [ValidateInput("@m_TBulletStartPositions.Length > 0", "There must be one start position at least")]
        private Transform[] m_TBulletStartPositions;
        
        [SerializeField] [Required]
        private Projectile m_Projectile;

        [SerializeField]
        private float m_ProjectileSpeed = 1.0f;
        
        public void Shot()
        {
            foreach (var startPos in m_TBulletStartPositions)
            {
                var right = startPos.right;
                var p = Instantiate(m_Projectile, startPos.position, Quaternion.Euler(right));
                p.Init(right * m_ProjectileSpeed, 0, lifeTime: 5.0f, author: transform.root.gameObject);
            }
        }
    }
}
 