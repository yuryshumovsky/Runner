using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Level;
using Zenject;

namespace Runner.Scripts.Gameplay.Handlers
{
    /// <summary>
    /// A handler for level step take events.
    /// This class is responsible for listening for level step take events and generating the next level step if the level generator defines a level step.
    /// </summary>
    public class LevelStepTakeHandler : IInitializable
    {
        [Inject] private LevelGenerateFacade _levelGenerateFacade;

        public void Initialize()
        {
            _levelGenerateFacade.OnTakeAway += Handler;
        }

        private void Handler(ILevelGenerator levelGenerator, ILevelItemView itemView)
        {
            if (levelGenerator.DefineLevelStep)
            {
                _levelGenerateFacade.GenerateNext();
            }
        }
    }
}