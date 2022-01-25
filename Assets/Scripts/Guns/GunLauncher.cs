using UnityEngine;

namespace DenizYanar.Guns
{
    public class GunLauncher : MonoBehaviour
    {
        [SerializeField] private Transform[] _bulletStartPositions;
        [SerializeField] private Projectile _projectile;
        
        
        public void Shot()
        {
            foreach (var startPos in _bulletStartPositions)
            {
                var right = startPos.right;
                var p = Instantiate(_projectile, startPos.position, Quaternion.Euler(right));
                p.Init(right * 35f, 0, lifeTime: 5.0f, author: transform.root.gameObject);
            }
        }
    }
}
 