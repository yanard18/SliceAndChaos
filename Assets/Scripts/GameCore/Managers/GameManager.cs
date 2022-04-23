using DenizYanar.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] [Required] 
        private VoidEvent m_ecSpawnPlayer;

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
