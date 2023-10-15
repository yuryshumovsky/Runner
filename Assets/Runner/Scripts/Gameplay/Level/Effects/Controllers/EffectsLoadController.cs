using System;
using System.Collections.Generic;
using Runner.Scripts.Configs;
using Runner.Scripts.Gameplay.Level.Effects.Configs;
using Runner.Scripts.Gameplay.Level.Effects.Data;
using Runner.Scripts.Gameplay.Level.Effects.ViewElements;
using Runner.Scripts.Gameplay.ObjectActivity;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Misc;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.Effects.Controllers
{
    /// <summary>
    /// A class that controls the loading of effects.
    /// </summary>
    public class EffectsLoadController : IInitController
    {
        [Inject] protected DiContainer Container { get; set; }
        [Inject] private GameModel _gameModel;
        [Inject] private GameConfig _gameGameConfig;

        public Action<IInitController> OnReady { get; set; }
        public Action<string> OnError { get; set; }

        private Dictionary<AsyncOperationHandle<GameObject>, EffectConfig> _loadDict = new();
        private List<string> loadingList = new();


        public void Init()
        {
            _gameModel.levelEffectsFactoryDict.Clear();

            foreach (LevelObjectConfig objectConfig in _gameGameConfig.levelObjects)
            {
                foreach (LevelItemBaseActivity itemActivity in objectConfig.takeActivities)
                {
                    if (itemActivity is PlayObjectAnimationActivity playObjectAnimationActivity)
                    {
                        if (loadingList.Contains(playObjectAnimationActivity.effectConfig.effectReference.AssetGUID))
                        {
                            continue;
                        }

                        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(
                            playObjectAnimationActivity.effectConfig.effectReference
                        );
                        
                        handle.Completed += LoadDoneHandler;

                        _loadDict.Add(handle, playObjectAnimationActivity.effectConfig);
                        loadingList.Add(playObjectAnimationActivity.effectConfig.effectReference.AssetGUID);
                    }
                }
            }
        }

        private void LoadDoneHandler(AsyncOperationHandle<GameObject> handle)
        {
            LevelEffectView objectView = handle.Result.gameObject.GetComponent<LevelEffectView>();

            EffectConfig config = _loadDict[handle];

            BindPlatform(config.ID, objectView);
            _gameModel.levelEffectsFactoryDict.Add(config.ID, Container.ResolveId<LevelEffectView.Factory>(config.ID));

            _loadDict.Remove(handle);
            if (_loadDict.Count == 0)
            {
                OnReady?.Invoke(this);
            }
        }

        private void BindPlatform(string id, LevelEffectView prefab)
        {
            Container.BindFactory<LevelEffectParams, LevelEffectView, LevelEffectView.Factory>()
                .WithId(id)
                .FromPoolableMemoryPool<LevelEffectParams, LevelEffectView, LevelEffectView.FacadePool>(poolBinder =>
                    poolBinder
                        .WithInitialSize(1)
                        .FromNewComponentOnNewPrefab(prefab)
                        .WithGameObjectName("Effect_" + id)
                        .UnderTransformGroup("EffectsContainer")
                )
                .NonLazy();
        }
    }
}