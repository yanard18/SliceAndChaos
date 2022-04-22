using System;
using UnityEngine;

namespace DenizYanar.LevelManagement
{
    [CreateAssetMenu(menuName = "Level Management/Load Level Event Channel")]
    public class LoadLevelEvent : ScriptableObject
    {
        public event Action<MasterLevel> e_OnLoadLevelRequested;

        public void Invoke(MasterLevel level) => e_OnLoadLevelRequested?.Invoke(level);
    }
}
