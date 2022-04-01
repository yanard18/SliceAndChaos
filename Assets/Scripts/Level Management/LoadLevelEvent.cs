using System;
using UnityEngine;

namespace DenizYanar.LevelManagement
{
    [CreateAssetMenu(menuName = "Level Management/Load Level Event Channel")]
    public class LoadLevelEvent : ScriptableObject
    {
        public event Action<MasterLevel> OnLoadLevelRequested;

        public void Invoke(MasterLevel level) => OnLoadLevelRequested?.Invoke(level);
    }
}
