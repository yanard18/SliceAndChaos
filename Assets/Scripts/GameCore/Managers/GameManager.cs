using DenizYanar.Events;
using DenizYanar.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] [Required]
        private VoidEvent m_ecGameOver;

            
        public void GameOver()
        {
            m_ecGameOver.Invoke();
            Debug.Log("Game Over!");
        }
    }
}
