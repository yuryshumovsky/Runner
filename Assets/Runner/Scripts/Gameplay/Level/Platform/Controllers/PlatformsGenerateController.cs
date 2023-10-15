using System;
using System.Collections.Generic;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Level.Platform.ViewElements;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Misc;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.Platform.Controllers
{
    public class PlatformsGenerateController : IInitController, ILevelGenerator
    {
        [Inject] private GameModel _gameModel;

        public Action<IInitController> OnReady { get; set; }
        public Action<string> OnError { get; set; }
        public Action<ILevelGenerator, ILevelItemView> OnTake { get; set; }
        public bool DefineLevelStep { get; } = true;

        private List<PlatformView> _platformViews = new();

        public Action OnTriggerPlayer;
        public Action OnKillSomeone;

        private PlatformLevelFactory _platformLevelFactory;
        private Dictionary<bool, Color> _colorDict;
        private bool colorBool = true;

        public void Init()
        {
            _platformLevelFactory = new PlatformLevelFactory(_gameModel.levelPlatformFactoryDict);
            _colorDict = new Dictionary<bool, Color>()
            {
                {true, Color.white},
                {false, Color.gray}
            };

            OnReady?.Invoke(this);
        }

        private void PassPlatformHandler(PlatformView platformView)
        {
            OnTake?.Invoke(this, platformView);
        }

        public void DeleteLastOne()
        {
            PlatformView first = _platformViews[0];

            first.OnPassPlatform -= PassPlatformHandler;
            first.OnTriggerPlayer -= TriggerPlayerHandler;
            first.OnKillSomeone -= KillSomeoneHandler;

            first.Dispose();
            _platformViews.Remove(first);
        }

        public float GenerateNewOne(float position)
        {
            (PlatformView itemView, string type) result = _platformLevelFactory.GetNewOne();

            result.itemView.PlatformName = result.type;


            result.itemView.Color = _colorDict[colorBool];
            colorBool = !colorBool;

            result.itemView.OnPassPlatform += PassPlatformHandler;
            result.itemView.OnTriggerPlayer += TriggerPlayerHandler;
            result.itemView.OnKillSomeone += KillSomeoneHandler;

            _platformViews.Add(result.itemView);
            result.itemView.Position = new Vector3(0, 0, position);

            Vector3 size = result.itemView.Size;

            return position + size.z;
        }

        private void KillSomeoneHandler()
        {
            OnKillSomeone?.Invoke();
        }

        private void TriggerPlayerHandler()
        {
            OnTriggerPlayer?.Invoke();
        }
    }
}