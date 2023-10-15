using System;
using Runner.Scripts.Configs;
using Runner.Scripts.Gameplay.Character.ViewElements;
using Runner.Scripts.Gameplay.ViewElements;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Misc;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Runner.Scripts.Gameplay.Character.Controllers
{
    /// <summary>
    /// Load and spawns the game's character.
    /// </summary>
    public class SpawnCharacterController : IInitController
    {
        [Inject] private GameModel _gameModel;
        [Inject] private GameConfig _gameGameConfig;
        [Inject(Id = "character")] private SpawnPointView _characterContainer;

        public Action<IInitController> OnReady { get; set; }
        public Action<string> OnError { get; set; }

        public void Init()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(
                _gameGameConfig.characterAsset,
                _characterContainer.transform
            );

            handle.Completed += Handler;
        }

        private void Handler(AsyncOperationHandle<GameObject> obj)
        {
            _gameModel.CurrentCharacter = obj.Result.gameObject.GetComponent<CharacterView>();
            OnReady?.Invoke(this);
        }
    }
}