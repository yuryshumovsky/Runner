using System;
using System.Collections.Generic;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Interfaces;
using Zenject;

namespace Runner.Scripts.Gameplay.Level
{
    public class LevelGenerateFacade : IDisposable, IInitController
    {
        [Inject] private List<ILevelGenerator> _levelGenerators;
        private float _lastPosition = 0;

        public event Action<ILevelGenerator, ILevelItemView> OnTakeAway;

        public Action<IInitController> OnReady { get; set; }
        public Action<string> OnError { get; set; }

        public void Init()
        {
            foreach (ILevelGenerator levelGenerator in _levelGenerators)
            {
                levelGenerator.OnTake += GeneratorTakeHandler;
            }

            //pre-spawn items at start
            for (int i = 0; i < 30; i++)
            {
                GenerateNext(false);
            }

            OnReady?.Invoke(this);
        }

        private void GeneratorTakeHandler(ILevelGenerator levelGenerator, ILevelItemView levelItemView)
        {
            OnTakeAway?.Invoke(levelGenerator, levelItemView);
        }

        public void GenerateNext(bool deleteLast = true)
        {
            foreach (ILevelGenerator levelGenerator in _levelGenerators)
            {
                if (deleteLast)
                {
                    levelGenerator.DeleteLastOne();
                }

                float position = levelGenerator.GenerateNewOne(_lastPosition);
                if (levelGenerator.DefineLevelStep)
                {
                    _lastPosition = position;
                }
            }
        }

        public void Dispose()
        {
        }
    }
}