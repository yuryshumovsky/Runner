using System;
using System.Collections;
using System.Collections.Generic;
using Runner.Scripts.Gameplay.Character.Controllers;
using Runner.Scripts.Gameplay.Level;
using Runner.Scripts.Gameplay.Level.Effects.Controllers;
using Runner.Scripts.Gameplay.Level.LevelObjects.Controllers;
using Runner.Scripts.Gameplay.Level.Platform.Controllers;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.GameplayStates
{
    /// <summary>
    /// The gameplay level generation state.
    /// This state is responsible for generating and initializing the game level,
    /// including the player character, platforms, and other objects.
    /// </summary>
    public class GameplayGenerateLevelState : IGameState
    {
        [Inject] private CoroutineHelper _coroutineHelper;
        [Inject] private GameplayStateManager _gameplayStateManager;
        [Inject] private SpawnCharacterController _spawnCharacterController;
        [Inject] private LevelPlatformsLoadController _levelPlatformsLoadController;
        [Inject] private LevelObjectsLoadController _levelObjectsLoadController;
        [Inject] private PlatformsGenerateController _platformsGenerate;
        [Inject] private LevelObjectsController _levelObjectsController;

        [Inject] private LevelGenerateFacade _levelGenerateFacade;
        [Inject] private EffectsLoadController _effectsLoadController;

        private List<IInitController> initControllers;

        public void EnterState()
        {
            initControllers = new List<IInitController>()
            {
                _spawnCharacterController,
                _levelPlatformsLoadController,
                _levelObjectsLoadController,
                _platformsGenerate,
                _levelObjectsController,
                _levelGenerateFacade,
                _effectsLoadController
            };

            LoadNextInitController();
        }

        private void LoadNextInitController()
        {
            IInitController initController = initControllers[0];

            initController.OnError += ControllerLoadErrorHandler;
            initController.OnReady += ControllerReadyHandler;

            _coroutineHelper.StartCoroutine(InitCoroutine());

            IEnumerator InitCoroutine()
            {
                yield return null;
                try
                {
                    initController.Init();
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                    Debug.LogError($"show error {exception.Message} element {initController.GetType()}");
                }
            }
        }

        private void ControllerLoadErrorHandler(string e)
        {
        }

        private void ControllerReadyHandler(IInitController initController)
        {
            initController.OnError -= ControllerLoadErrorHandler;
            initController.OnReady -= ControllerReadyHandler;
            initControllers.Remove(initController);
            _coroutineHelper.StartCoroutine(CheckNextController());
        }

        private IEnumerator CheckNextController()
        {
            yield return null;

            if (initControllers.Count > 0)
            {
                LoadNextInitController();
            }
            else
            {
                AllControllersInitedHandler();
            }
        }

        private void AllControllersInitedHandler()
        {
            _gameplayStateManager.ChangeState(GamelpayState.Core);
        }

        public void ExitState()
        {
            //  _loadingScreen.Visible = false;
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }
    }
}