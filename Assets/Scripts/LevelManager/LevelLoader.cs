using UnityEngine;

namespace DenizYanar.LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private LoadLevelEvent _loadLevelEvent;

        private void Start() => LoadLevel();
        
        public void LoadLevel() => _loadLevelEvent.Invoke(_level);
    }
}
