using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.LevelManagement
{
    public class PlayerSpawnPosition : MonoBehaviour
    {
        
        [SerializeField] [Required] [AssetsOnly]
        private GameObject m_Player;
        
        [Required] [SerializeField] [AssetsOnly]
        private GameObject m_PlayerCamera;
        
        [SerializeField] [Required] [AssetsOnly]
        private GameObject m_VirtualCamera;
        
        public void SpawnPlayer()
        {
            var position = transform.position;            
            var playerObj = Instantiate(m_Player, position, Quaternion.identity);
            var camObj = Instantiate(m_PlayerCamera, position, Quaternion.identity);
            var cinemachineObj = Instantiate(m_VirtualCamera, position, Quaternion.identity);
            var virtualCamera = cinemachineObj.GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = playerObj.transform;

        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 1.0f);
        }
        
        
    }
}
