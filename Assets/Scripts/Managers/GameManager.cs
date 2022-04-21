using DenizYanar.Events;
using UnityEngine;

namespace DenizYanar.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO m_ecSpawnPlayer;

        public void OnLevelReady()
        {
            SpawnPlayer();
        }
        
        private void SpawnPlayer()
        {
            m_ecSpawnPlayer.Invoke();
        }
        
        public void GameOver()
        {
            Debug.Log("Game Over!");
        }
    }
}
