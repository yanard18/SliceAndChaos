using System.Collections;
using System.Collections.Generic;
using DenizYanar.Events;
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
        
        public float LoadingProgress { get; private set; }

        private void OnEnable() => _loadLevelEvent.OnLoadLevelRequested += LoadLevel;
        private void OnDisable() => _loadLevelEvent.OnLoadLevelRequested -= LoadLevel;

        private void LoadLevel(MasterLevel masterLevel)
        {
            //Unload Current Level
            if (_currentLevel != null)
            {
                _loadingOperations.Add(SceneManager.UnloadSceneAsync(_currentLevel.LevelName));
                
                foreach (var dependencyLevel in _currentLevel.DependencyList.LevelList)
                {
                    _loadingOperations.Add(SceneManager.UnloadSceneAsync(dependencyLevel.LevelName));
                }
            }


            //Load Dependency Levels
            foreach (var dependencyLevel in masterLevel.DependencyList.LevelList)
            {
                if(!SceneManager.GetSceneByName(dependencyLevel.LevelName).isLoaded) 
                    _loadingOperations.Add(SceneManager.LoadSceneAsync(dependencyLevel.LevelName, LoadSceneMode.Additive));
            }
            //Load Master Level
            _loadingOperations.Add(SceneManager.LoadSceneAsync(masterLevel.LevelName, LoadSceneMode.Additive));

            StartCoroutine(nameof(GetSceneLoadProgress));
            
            _currentLevel = masterLevel;
            LoadingProgress = 0;

        }
        

        private IEnumerator GetSceneLoadProgress()
        {
            foreach (var op in _loadingOperations)
            {
                while (!op.isDone)
                {
                    LoadingProgress = 0;
                    foreach (var operation in _loadingOperations)
                    {
                        LoadingProgress += operation.progress;
                    }

                    LoadingProgress = (LoadingProgress / _loadingOperations.Count) * 100f;

                    yield return null;
                }
            }
        }
        
        
        
        

        
    }
}