using System.Collections.Generic;
using Runner.Scripts.AppStates.Services;
using Runner.Scripts.Interfaces;
using Zenject;

namespace Runner.Scripts.Gameplay.GameplayStates
{
    /// <summary>
    /// The gameplay state manager. This class is responsible for managing the state of the gameplay system.
    /// It can change the state of the gameplay system to one of the following:
    /// - GamelpayState.GenerateLevel: This state is responsible for generating and initializing the game level.
    /// - GamelpayState.Core: This state is responsible for the core gameplay functionality, such as player movement and interaction with the environment.
    /// </summary>
    public class GameplayStateManager : BaseStateManager<GamelpayState>, IInitializable
    {
        //states
        [Inject] private GameplayGenerateLevelState _generateLevelState;
        [Inject] private GameplayCoreState _gameplayCoreState;
        //end states

        public void Initialize()
        {
            _states = new Dictionary<GamelpayState, IGameState>
            {
                {GamelpayState.GenerateLevel, _generateLevelState},
                {GamelpayState.Core, _gameplayCoreState},
            };

            ChangeState(GamelpayState.GenerateLevel);
        }
    }

    public enum GamelpayState
    {
        None,
        GenerateLevel,
        Core
    }
}