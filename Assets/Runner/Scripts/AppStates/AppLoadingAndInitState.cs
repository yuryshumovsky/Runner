using Runner.Scripts.AppStates.Services;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Misc.Loader;
using Zenject;

namespace Runner.Scripts.AppStates
{
    /// <summary>
    /// Represents the application loading and initialization process.
    /// </summary>
    public class AppLoadingAndInitState : IGameState
    {
        [Inject] private AppStateManager _appStateManager;
        [Inject] private LoadingScreen _loadingScreen;

        public void EnterState()
        {
            _loadingScreen.Visible = true;
            _appStateManager.ChangeState(Services.AppStates.Gameplay);
        }


        public void ExitState()
        {
            //
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }
    }
}