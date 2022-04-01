using DenizYanar.Events;
using UnityEngine;

namespace DenizYanar.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _spawnPlayerEvent;

        public void OnLevelReady()
        {
            SpawnPlayer();
        }
        
        private void SpawnPlayer()
        {
            _spawnPlayerEvent.Invoke();
        }
        
        public void GameOver()
        {
            Debug.Log("Game Over!");
        }
    }
}
