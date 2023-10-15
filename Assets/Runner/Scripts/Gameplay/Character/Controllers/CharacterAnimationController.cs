using Runner.Scripts.Misc;
using Zenject;

namespace Runner.Scripts.Gameplay.Character.Controllers
{
    /// <summary>
    /// Controls the animations of the game's character.
    /// </summary>
    public class CharacterAnimationController
    {
        [Inject] private GameModel _gameModel;

        public void PlayRun()
        {
            _gameModel.CurrentCharacter.SetAnimationKey("run", true);
        }

        public void PlayJump()
        {
            _gameModel.CurrentCharacter.SetAnimationKey("jump", true);
        }

        public void StopJump()
        {
            _gameModel.CurrentCharacter.SetAnimationKey("jump", false);
        }
    }
}