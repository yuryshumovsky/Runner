using System;
using System.Collections.Generic;
using Runner.Scripts.Configs;
using Runner.Scripts.Gameplay.Level.LevelObjects.Data;
using Runner.Scripts.Gameplay.Level.LevelObjects.ViewElements;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Misc;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.LevelObjects.Controllers
{
    /// <summary>
    /// A class that controls the loading of level objects.
    /// </summary>
    public class LevelObjectsLoadController : IInitController
    {
        [Inject] protected DiContainer Container { get; set; }
        [Inject] private GameModel _gameModel;
        [Inject] private GameConfig _gameGameConfig;

        public Action<IInitController> OnReady { get; set; }
        public Action<string> OnError { get; set; }

        private Dictionary<AsyncOperationHandle<GameObject>, LevelObjectConfig> _loadDict = new();
        private List<string> alreadyLoadingAssetsList = new();

        public void Init()
        {
            _gameModel.levelObjectFactoryDict.Clear();

            foreach (LevelObjectConfig objectConfig in _gameGameConfig.levelObjects)
            {
                if (alreadyLoadingAssetsList.Contains(objectConfig.levelAssetConfig.assetKey))
                {
                    continue;
                }

                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(
                    objectConfig.levelAssetConfig.assetReference
                );

                _loadDict.Add(handle, objectConfig);
                alreadyLoadingAssetsList.Add(objectConfig.levelAssetConfig.assetKey);

                handle.Completed += LoadDoneHandler;
            }
        }

        private void LoadDoneHandler(AsyncOperationHandle<GameObject> handle)
        {
            LevelObjectView objectView = handle.Result.gameObject.GetComponent<LevelObjectView>();

            LevelObjectConfig config = _loadDict[handle];
            BindPlatform(config.levelAssetConfig.assetKey, objectView);

            _gameModel.levelObjectFactoryDict.Add(
                config.levelAssetConfig.assetKey,
                Container.ResolveId<LevelObjectView.Factory>(config.levelAssetConfig.assetKey)
            );

            _loadDict.Remove(handle);
            if (_loadDict.Count == 0)
            {
                OnReady?.Invoke(this);
            }
        }

        private void BindPlatform(string assetKey, LevelObjectView prefab)
        {
            Container.BindFactory<LevelObjectParams, LevelObjectView, LevelObjectView.Factory>()
                .WithId(assetKey)
                .FromPoolableMemoryPool<LevelObjectParams, LevelObjectView, LevelObjectView.FacadePool>(poolBinder =>
                    poolBinder
                        .WithInitialSize(5)
                        .FromNewComponentOnNewPrefab(prefab)
                        .WithGameObjectName("Object_" + assetKey)
                        .UnderTransformGroup("ObjectsContainer")
                )
                .NonLazy();
        }
    }
}