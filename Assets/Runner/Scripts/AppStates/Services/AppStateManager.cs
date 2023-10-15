using System.Collections.Generic;
using Runner.Scripts.Interfaces;
using Zenject;

namespace Runner.Scripts.AppStates.Services
{
    /// <summary>
    /// Defines states for the game and allows to change those states.
    /// The default state at start is AppStates.AppLoadingAndInit
    /// </summary>
    public class AppStateManager : BaseStateManager<AppStates>, IInitializable
    {
        [Inject] private AppLoadingAndInitState _appLoadingAndInitState;
        [Inject] private GameplayState _playMechanicState;

        public void Initialize()
        {
            _states = new Dictionary<AppStates, IGameState>
            {
                {AppStates.AppLoadingAndInit, _appLoadingAndInitState},
                {AppStates.Gameplay, _playMechanicState}
            };

            ChangeState(AppStates.AppLoadingAndInit);
        }
    }

    public enum AppStates
    {
        None,
        AppLoadingAndInit,
        Gameplay
    }
}