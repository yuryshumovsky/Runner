using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runner.Scripts.Utils
{
    public class SceneLoader
    {
        public void LoadScene(string sceneName, LoadSceneMode parameters, Action<AsyncOperation> OnSceneLoad)
        {

            if (!IsSceneActive(sceneName))
            {
                AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, parameters);
                async.completed += OnSceneLoad;
                async.allowSceneActivation = true;
            }
            else
            {
                OnSceneLoad?.Invoke(null);
            }
        }

        public bool IsSceneActive(string sceneName)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneName))
            {
                return true;
            }

            return false;
        }

        public bool IsSceneLoaded(string name)
        {
            var scene = SceneManager.GetSceneByName(name);
            return scene.isLoaded;
        }

        public void ActivateScene(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);
        }
        
        protected void UnloadScene(string sceneName)
        {
            if (IsSceneLoaded(sceneName))
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}