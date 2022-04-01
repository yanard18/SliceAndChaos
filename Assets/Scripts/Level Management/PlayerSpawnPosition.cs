using Cinemachine;
using UnityEngine;

namespace DenizYanar.LevelManagement
{
    public class PlayerSpawnPosition : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _playerCamera;
        [SerializeField] private GameObject _virtualCamera;
        
        public void SpawnPlayer()
        {
            var position = transform.position;            
            var playerObj = Instantiate(_player, position, Quaternion.identity);
            var camObj = Instantiate(_playerCamera, position, Quaternion.identity);
            var cinemachineObj = Instantiate(_virtualCamera, position, Quaternion.identity);
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
