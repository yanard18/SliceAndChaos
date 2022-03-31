using System.Collections;
using System.Collections.Generic;
using DenizYanar.Events;
using DenizYanar.GameVariables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DenizYanar.LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LoadLevelEvent _loadLevelEvent;
        [SerializeField] private VoidEventChannelSO _levelReadyEvent;
        
        private readonly List<AsyncOperation> _loadingOperations = new List<AsyncOperation>();

        private MasterLevel _currentLevel;

        public FloatVariable LoadingProgress;

        private void OnEnable() => _loadLevelEvent.OnLoadLevelRequested += LoadLevel;
        private void OnDisable() => _loadLevelEvent.OnLoadLevelRequested -= LoadLevel;

        private void LoadLevel(MasterLevel masterLevel)
        {
            UnloadCurrentLevel();
            LoadNewLevel(masterLevel);
            HandleLoadingProgress(masterLevel);
            
            _currentLevel = masterLevel;
        }

        private void OnLevelLoaded(Level level)
        {
            LoadingProgress.Value = 0;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(level.LevelName));

            //_levelReadyEvent.Invoke();
        }

        private void HandleLoadingProgress(MasterLevel level)
        {
            StartCoroutine(GetSceneLoadProgress(level));
        }

        private void LoadNewLevel(MasterLevel masterLevel)
        {
            LoadLevelDependencies(masterLevel);
            LoadMasterLevel(masterLevel);
        }

        private void LoadMasterLevel(Level masterLevel)
        {
            _loadingOperations.Add(SceneManager.LoadSceneAsync(masterLevel.LevelName, LoadSceneMode.Additive));
        }

        private void LoadLevelDependencies(MasterLevel masterLevel)
        {
            foreach (var dependencyLevel in masterLevel.DependencyList.LevelList)
            {
                //if (!LevelExist(dependencyLevel))
                    _loadingOperations.Add(SceneManager.LoadSceneAsync(dependencyLevel.LevelName, LoadSceneMode.Additive));
            }
        }

        private static bool LevelExist(Level dependencyLevel)
        {
            return SceneManager.GetSceneByName(dependencyLevel.LevelName).isLoaded;
        }

        private void UnloadCurrentLevel()
        {
            if (ThereAreNoActiveLevel) return;
            
            UnloadMasterLevel();
            UnloadLevelDependencies();
        }

        private void UnloadMasterLevel()
        {
            _loadingOperations.Add(SceneManager.UnloadSceneAsync(_currentLevel.LevelName));
        }

        private void UnloadLevelDependencies()
        {
            foreach (var dependencyLevel in _currentLevel.DependencyList.LevelList)
                _loadingOperations.Add(SceneManager.UnloadSceneAsync(dependencyLevel.LevelName));
        }

        private bool ThereAreNoActiveLevel => _currentLevel == null;


        private IEnumerator GetSceneLoadProgress(Level level)
        {
            foreach (var op in _loadingOperations)
            {
                while (!op.isDone)
                {
                    LoadingProgress.Value = 0;
                    foreach (var operation in _loadingOperations)
                    {
                        LoadingProgress.Value += operation.progress;
                    }

                    LoadingProgress.Value = (LoadingProgress.Value / _loadingOperations.Count) * 100f;
                    Debug.Log(LoadingProgress.Value);

                    yield return null;
                }
            }
            OnLevelLoaded(level);
        }
        
        
        
        

        
    }
}