using System.Collections;
using Runner.Scripts.Gameplay.Character.Controllers;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Level.LevelObjects.Controllers;
using Runner.Scripts.Misc;
using Runner.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.ObjectActivity
{
    [CreateAssetMenu(fileName = "FlyPlayerActivity", menuName = "Create 'FlyPlayerActivity'", order = 0)]
    public class FlyPlayerActivity : LevelItemBaseActivity
    {
        [Inject] private CharacterAnimationController _characterAnimationController;
        [Inject] private GameModel _gameModel;
        [Inject] private CoroutineHelper _coroutineHelper;
        [Inject] private LevelObjectsController _levelObjectsController;

        public int time;

        public override void Run(ILevelItemView itemView)
        {
            _characterAnimationController.PlayJump();
            _gameModel.CurrentCharacter.Jump(new Vector3(0, 280f, 0));
            _gameModel.jumpState = JumpState.SINGE;
            _levelObjectsController.Enable = false;

            _coroutineHelper.StartCoroutine(CheckFlyModeCoroutine());
            _coroutineHelper.StartCoroutine(EndFlyCoroutine());
        }

        private IEnumerator EndFlyCoroutine()
        {
            yield return new WaitForSeconds(time);

            _gameModel.CurrentCharacter.UseGravity = true;
            _levelObjectsController.Enable = true;
        }

        private IEnumerator CheckFlyModeCoroutine()
        {
            float prevY = _gameModel.CurrentCharacter.Position.y;
            while (true)
            {
                if (_gameModel.CurrentCharacter.Position.y < prevY)
                {
                    _gameModel.CurrentCharacter.UseGravity = false;
                    yield break;
                }

                prevY = _gameModel.CurrentCharacter.Position.y;

                yield return null;
            }
        }
    }
}