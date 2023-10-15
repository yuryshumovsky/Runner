using System;
using System.Linq;
using Runner.Scripts.Configs;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Level.LevelObjects.Controllers;
using Runner.Scripts.Gameplay.ObjectActivity;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.LevelObjects.Handlers
{
    /// <summary>
    /// A class that handles the triggering of level objects by the player.
    /// </summary>
    public class LevelObjectTriggerPlayerHandler : IInitializable, IDisposable
    {
        [Inject] private LevelObjectsController _levelObjectsController;
        [Inject] private GameConfig _gameConfig;
        [Inject] private DiContainer _container;

        public void Initialize()
        {
            _levelObjectsController.OnTriggerPlayer += Handler;
        }

        private void Handler(ILevelItemView itemView)
        {
            LevelObjectConfig config = _gameConfig.levelObjects
                .FirstOrDefault(item => item.ID == itemView.ID);

            foreach (LevelItemBaseActivity activity in config.takeActivities)
            {
                _container.Inject(activity);
                activity.Run(itemView);
            }
        }

        public void Dispose()
        {
            _levelObjectsController.OnTriggerPlayer -= Handler;
        }
    }
}