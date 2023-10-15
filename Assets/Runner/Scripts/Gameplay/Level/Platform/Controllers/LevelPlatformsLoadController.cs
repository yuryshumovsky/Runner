using System;
using System.Collections.Generic;
using Runner.Scripts.Configs;
using Runner.Scripts.Gameplay.Level.Platform.Data;
using Runner.Scripts.Gameplay.Level.Platform.ViewElements;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Misc;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.Platform.Controllers
{
    public class LevelPlatformsLoadController : IInitController
    {
        [Inject] protected DiContainer Container { get; set; }
        [Inject] private GameModel _gameModel;
        [Inject] private GameConfig _gameGameConfig;

        public Action<IInitController> OnReady { get; set; }
        public Action<string> OnError { get; set; }

        private Dictionary<AsyncOperationHandle<GameObject>, PlatformConfig> _loadDict = new();

        public void Init()
        {
            _gameModel.levelPlatformFactoryDict.Clear();

            foreach (PlatformConfig platformData in _gameGameConfig.levelPlatforms)
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(
                    platformData.platformView
                );
                _loadDict.Add(handle, platformData);

                handle.Completed += LoadDoneHandler;
            }
        }

        private void LoadDoneHandler(AsyncOperationHandle<GameObject> handle)
        {
            PlatformView platformView = handle.Result.gameObject.GetComponent<PlatformView>();

            BindPlatform(platformView.name, platformView);

            _loadDict.Remove(handle);
            if (_loadDict.Count == 0)
            {
                OnReady?.Invoke(this);
            }
        }

        private void BindPlatform(string name, PlatformView prefab)
        {
            Container.BindFactory<PlatformParams, PlatformView, PlatformView.Factory>()
                .WithId(name)
                .FromPoolableMemoryPool<PlatformParams, PlatformView, PlatformView.FacadePool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromNewComponentOnNewPrefab(prefab)
                    .WithGameObjectName("Platform_" + name)
                    .UnderTransformGroup("PlatformsContainer"))
                .NonLazy();

            _gameModel.levelPlatformFactoryDict.Add(name, Container.ResolveId<PlatformView.Factory>(name.ToString()));
        }
    }
}