using Runner.Scripts.Gameplay.Character.Controllers;
using Runner.Scripts.Gameplay.Misc;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Misc.Loader;
using Zenject;

namespace Runner.Scripts.Gameplay.GameplayStates
{
    /// <summary>
    /// The core gameplay state.
    /// This state is responsible for initializing and managing the core gameplay systems.
    /// </summary>
    public class GameplayCoreState : IGameState
    {
        [Inject] private LoadingScreen _loadingScreen;
        [Inject] private CharacterRunController _characterRunController;
        [Inject] private CharacterAnimationController _characterAnimationController;
        [Inject] private TouchAndMouseHandler _touchAndMouseHandler;

        public void EnterState()
        {
            _loadingScreen.Visible = false;
            _touchAndMouseHandler.Enable = true;
            _characterRunController.Enable = true;
            _characterAnimationController.PlayRun();
        }

        public void ExitState()
        {
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }
    }
}