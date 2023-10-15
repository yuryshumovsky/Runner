using Runner.Scripts.Gameplay.Character.Controllers;
using Runner.Scripts.Gameplay.Level.Platform.Controllers;
using Runner.Scripts.Misc;
using Zenject;

namespace Runner.Scripts.Gameplay.Handlers
{
    /// <summary>
    /// A handler for restoring run after jump events.
    /// This class is responsible for listening for restore run after jump events and restoring the character's run animation if the character is currently jumping.
    /// </summary>
    public class RestoreRunAfterJumpHandler : IInitializable
    {
        [Inject] private PlatformsGenerateController _platformsGenerateController;
        [Inject] private GameModel _gameModel;
        [Inject] private CharacterAnimationController _characterAnimationController;

        public void Initialize()
        {
            _platformsGenerateController.OnTriggerPlayer += Handler;
        }

        private void Handler()
        {
            if (_gameModel.jumpState == JumpState.SINGE || _gameModel.jumpState == JumpState.DOUBLE)
            {
                _gameModel.jumpState = JumpState.NONE;
                _characterAnimationController.StopJump();
            }
        }
    }
}