using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] [Required]
        private MasterLevel m_Level;
        
        [SerializeField] [Required]
        private LoadLevelEvent m_ecLoadLevel;

        [SerializeField] [Required]
        private bool m_bLoadAtStart;
        
        private void Start()
        {
            if(!m_bLoadAtStart) return;
            
            LoadLevel();
        }

        public void LoadLevel() => m_ecLoadLevel.Invoke(m_Level);
    }
}
