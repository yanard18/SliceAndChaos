using System.Collections;
using DenizYanar.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DenizYanar.LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LoadLevelEvent _loadLevelEvent;
        [SerializeField] private VoidEventChannelSO _levelReadyEvent;

        private Level _currentLevel;
        
        private void OnEnable() => _loadLevelEvent.OnLoadLevelRequested += LoadLevel;
        private void OnDisable() => _loadLevelEvent.OnLoadLevelRequested -= LoadLevel;

        private void LoadLevel(Level level)
        {
            if (_currentLevel != null && _currentLevel != level)
                UnloadLevel(level);
            
            if (SceneManager.GetSceneByName(level.LevelName).isLoaded) return;

            if (level.Dependencies != null && level.Dependencies.LevelList.Length > 0)
                LoadLevelDependencies(level.Dependencies);

            StartCoroutine(GetLoadProgress(level, setActiveScene: true));

            _currentLevel = level;
        }
        
        private void UnloadLevel(Level level)
        {
            if (!SceneManager.GetSceneByName(level.LevelName).isLoaded) return;
            
            if (level.Dependencies != null && level.Dependencies.LevelList.Length > 0)
                UnloadLevelDependencies(level.Dependencies);
            
            StartCoroutine(GetUnloadProgress(level));
        }

        private void LoadLevelDependencies(LevelDependencies dependencies)
        {
            foreach (var level in dependencies.LevelList)
            {
                if (SceneManager.GetSceneByName(level.LevelName).isLoaded) continue;

                StartCoroutine(GetLoadProgress(level));
            }
        }

        private void UnloadLevelDependencies(LevelDependencies dependencies)
        {
            foreach (var level in dependencies.LevelList)
            {
                if (!SceneManager.GetSceneByName(level.LevelName).isLoaded) continue;

                StartCoroutine(GetUnloadProgress(level));
            }
        }


        private IEnumerator GetLoadProgress(Level level, bool setActiveScene = false)
        {
            var operation = SceneManager.LoadSceneAsync(level.LevelName, LoadSceneMode.Additive);
            Debug.Log(level.LoadDescription);
            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);
                yield return null;
            }

            if (setActiveScene)
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(level.LevelName));
        }

        private IEnumerator GetUnloadProgress(Level level)
        {
            var operation = SceneManager.UnloadSceneAsync(level.LevelName);
            Debug.Log("Unloading Level: " + level.LevelName);
            while (!operation.isDone)
            {
                yield return null;
            }
        }

        
    }
}