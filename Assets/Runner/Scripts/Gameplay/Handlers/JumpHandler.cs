using Runner.Scripts.Gameplay.Character.Controllers;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Misc;
using Runner.Scripts.Misc;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Handlers
{
    /// <summary>
    /// A handler for jump events.
    /// This class is responsible for detecting jump events and playing the appropriate character animations.
    /// It also updates the game model to reflect the character's current jump state.
    /// </summary>
    public class JumpHandler : IInitializable, IScreenActionController
    {
        [Inject] private TouchAndMouseHandler _touchAndMouseHandler;
        [Inject] private CharacterAnimationController _characterAnimationController;
        [Inject] private GameModel _gameModel;

        public void Initialize()
        {
            _touchAndMouseHandler.SubscribeNewOne(this);
        }

        public void SingleDownOrTapHandler()
        {
            if (_gameModel.jumpState == JumpState.NONE)
            {
                _gameModel.jumpState = JumpState.SINGE;
                _characterAnimationController.PlayJump();
                _gameModel.CurrentCharacter.Jump(new Vector3(0, 220f, 20));
            }
        }

        public void DoubleDownOrTapHandler()
        {
            if (_gameModel.jumpState == JumpState.SINGE)
            {
                _gameModel.jumpState = JumpState.DOUBLE;
                _characterAnimationController.PlayJump();
                _gameModel.CurrentCharacter.Jump(new Vector3(0, 180f, 20));
            }
        }
    }
}