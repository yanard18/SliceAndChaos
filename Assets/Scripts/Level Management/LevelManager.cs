using System.Collections;
using System.Collections.Generic;
using DenizYanar.Events;
using DenizYanar.GameVariables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DenizYanar.LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] [Required]
        private LoadLevelEvent m_ecLoadLevel;
        
        [SerializeField] [Required]
        private VoidEventChannelSO m_ecLevelReady;
        
        private readonly List<AsyncOperation> m_TLoadingOperations = new ();

        private MasterLevel m_CurrentLevel;

        [Required]
        public FloatVariable m_LoadingProgressValue;

        private void OnEnable() => m_ecLoadLevel.e_OnLoadLevelRequested += LoadLevel;
        private void OnDisable() => m_ecLoadLevel.e_OnLoadLevelRequested -= LoadLevel;

        private void LoadLevel(MasterLevel masterLevel)
        {
            UnloadLevel(m_CurrentLevel);
            LoadNewLevel(masterLevel);
            HandleLoadingProgress(masterLevel);
            
            m_CurrentLevel = masterLevel;
        }

        private void OnLevelLoaded(Level level)
        {
            m_LoadingProgressValue.Value = 0;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(level.m_LevelName));
            
            m_ecLevelReady.Invoke();
        }

        private void HandleLoadingProgress(Level level) => StartCoroutine(GetSceneLoadProgress(level));

        private void LoadNewLevel(MasterLevel masterLevel)
        {
            LoadLevelDependencies(masterLevel);
            LoadMasterLevel(masterLevel);
        }

        private void LoadMasterLevel(Level masterLevel)
        {
            if(!LevelExist(masterLevel))
                m_TLoadingOperations.Add(SceneManager.LoadSceneAsync(masterLevel.m_LevelName, LoadSceneMode.Additive));
        }

        private void LoadLevelDependencies(MasterLevel masterLevel)
        {
            foreach (var dependencyLevel in masterLevel.m_DependencyList.m_LevelList)
            {
                if (!LevelExist(dependencyLevel))
                    m_TLoadingOperations.Add(SceneManager.LoadSceneAsync(dependencyLevel.m_LevelName, LoadSceneMode.Additive));
            }
        }

        private void UnloadLevel(MasterLevel level)
        {
            if (ThereAreNoActiveLevel) return;
            
            UnloadMasterLevel(level);
            UnloadLevelDependencies(level);
        }

        private void UnloadMasterLevel(Level level)
        {
            m_TLoadingOperations.Add(SceneManager.UnloadSceneAsync(level.m_LevelName));
        }

        private void UnloadLevelDependencies(MasterLevel level)
        {
            foreach (var dependencyLevel in level.m_DependencyList.m_LevelList)
                m_TLoadingOperations.Add(SceneManager.UnloadSceneAsync(dependencyLevel.m_LevelName));
        }

        


        private IEnumerator GetSceneLoadProgress(Level level)
        {
            foreach (var op in m_TLoadingOperations)
            {

                while (!op.isDone)
                {
                    m_LoadingProgressValue.Value = 0;
                    foreach (var operation in m_TLoadingOperations)
                    {
                        m_LoadingProgressValue.Value += operation.progress;
                    }

                    m_LoadingProgressValue.Value = (m_LoadingProgressValue.Value / m_TLoadingOperations.Count) * 100f;

                    yield return null;
                }
            }
            OnLevelLoaded(level);
        }
        
        
        
        private static bool LevelExist(Level dependencyLevel) => SceneManager.GetSceneByName(dependencyLevel.m_LevelName).isLoaded;

        private bool ThereAreNoActiveLevel => m_CurrentLevel == null;
        
    }
}