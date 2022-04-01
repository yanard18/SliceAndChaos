using UnityEngine;

namespace DenizYanar.LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private MasterLevel _level;
        [SerializeField] private LoadLevelEvent _loadLevelEvent;

        [SerializeField] private bool _loadAtStart;
        
        private void Start()
        {
            if(!_loadAtStart) return;
            
            LoadLevel();
        }

        public void LoadLevel() => _loadLevelEvent.Invoke(_level);
    }
}
