using System;
using System.Collections.Generic;
using System.Linq;
using Runner.Scripts.Configs;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Level.LevelObjects.ViewElements;
using Runner.Scripts.Gameplay.Level.Platform.ViewElements;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Misc;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.LevelObjects.Controllers
{
    /// <summary>
    /// A class that controls level objects.
    /// </summary>
    public class LevelObjectsController : IInitController, ILevelGenerator
    {
        [Inject] private GameModel _gameModel;
        [Inject] private GameConfig _gameConfig;

        public Action<IInitController> OnReady { get; set; }
        public Action<string> OnError { get; set; }
        public Action<ILevelGenerator, ILevelItemView> OnTake { get; set; }
        public bool DefineLevelStep { get; } = false;
        public bool Enable { get; set; } = true;
        private List<LevelObjectView> _itemsViews = new();
        private float _lastPlatformPosition = 0f;
        public Action<ILevelItemView> OnTriggerPlayer;
        private LevelObjectsFactory _itemsFactory;


        public void Init()
        {
            _itemsFactory = new LevelObjectsFactory(
                _gameModel.levelObjectFactoryDict,
                _gameConfig.levelObjects
                );

            OnReady?.Invoke(this);
        }

        private void PassPlatformHandler(PlatformView platformView)
        {
            OnTake?.Invoke(this, platformView);
        }

        public void DeleteLastOne()
        {
            if (_itemsViews[0] == null)
            {
                _itemsViews.RemoveAt(0);
                return;
            }

            LevelObjectView first = _itemsViews[0];

            //first.OnPassPlatform -= PassPlatformHandler;
            first.OnTriggerPlayer -= TriggerPlayerHandler;
            //first.OnKillSomeone -= KillSomeoneHandler;

            first.Dispose();
            _itemsViews.Remove(first);
        }

        public float GenerateNewOne(float position)
        {
            (LevelObjectView itemView, string type) result = _itemsFactory.GetNewOne();

            _itemsViews.Add(result.itemView);
            if (result.itemView)
            {
                result.itemView.Position = new Vector3(0, 0.4f, position);
                result.itemView.OnTriggerPlayer += TriggerPlayerHandler;

                var config = _gameConfig.levelObjects.FirstOrDefault(item => item.ID == result.itemView.ID);
                result.itemView.Color = config.Color;
            }
            
            return -1;
        }

        private void TriggerPlayerHandler(LevelObjectView itemView)
        {
            if (Enable)
            {
                OnTriggerPlayer?.Invoke(itemView);
            }
        }
    }
}