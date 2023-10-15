using Runner.Scripts.Configs;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Runner.Scripts.AppStates
{
    /// <summary>
    ///The GameplayState class is responsible for managing the gameplay of the game.
    /// It does this by loading the game scene, initializing the gameplay state
    /// </summary>
    public class GameplayState : IGameState
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private SceneLoader _sceneLoader;

        public void EnterState()
        {
            _sceneLoader.LoadScene(_gameConfig.GameSceneName, LoadSceneMode.Single, OnLoadSceneHandler);
        }

        private void OnLoadSceneHandler(AsyncOperation obj)
        {
            //
        }

        public void ExitState()
        {
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }
    }
}