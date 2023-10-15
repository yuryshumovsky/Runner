using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Level.Effects.Configs;
using Runner.Scripts.Gameplay.Level.Effects.Controllers;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.ObjectActivity
{
    [CreateAssetMenu(fileName = "PlayObjectAnimationActivity", menuName = "Create 'PlayObjectAnimationActivity'",
        order = 0)]
    public class PlayObjectAnimationActivity : LevelItemBaseActivity
    {
        [Inject] private EffectPlayController _effectPlayController;

        public EffectConfig effectConfig;

        public override void Run(ILevelItemView itemView)
        {
            _effectPlayController.PlayAnimation(effectConfig.ID, itemView.Position);
        }
    }
}