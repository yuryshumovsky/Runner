using System.Collections;
using Runner.Scripts.Gameplay.Character.Controllers;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.ObjectActivity
{
    [CreateAssetMenu(fileName = "SpeedFactorPlayerActivity", menuName = "Create 'SpeedFactorPlayerActivity'",
        order = 0)]
    public class SpeedFactorPlayerActivity : LevelItemBaseActivity
    {
        [Inject] private CharacterRunController _characterRunController;
        [Inject] private CoroutineHelper _coroutineHelper;

        public int time;
        public float factor;

        public override void Run(ILevelItemView itemView)
        {
            _characterRunController.SpeedFactor = factor;
            _coroutineHelper.StartCoroutine(OffActivityCoroutine());
        }

        private IEnumerator OffActivityCoroutine()
        {
            yield return new WaitForSeconds(time);
            _characterRunController.SpeedFactor = 1f;
        }
    }
}