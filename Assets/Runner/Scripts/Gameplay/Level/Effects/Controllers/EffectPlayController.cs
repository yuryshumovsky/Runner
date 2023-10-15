using Runner.Scripts.Gameplay.Level.Effects.Data;
using Runner.Scripts.Gameplay.Level.Effects.ViewElements;
using Runner.Scripts.Misc;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.Effects.Controllers
{
    /// <summary>
    /// A class that controls the playing of effects.
    /// </summary>
    public class EffectPlayController
    {
        [Inject] private GameModel _gameModel;

        public void PlayAnimation(string id, Vector3 position)
        {
            LevelEffectView.Factory factory = _gameModel.levelEffectsFactoryDict[id];
            LevelEffectView itemView = factory.Create(new LevelEffectParams() { });
            itemView.Position = position;
        }
    }
}