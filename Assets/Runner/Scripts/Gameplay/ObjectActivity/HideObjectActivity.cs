using Runner.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace Runner.Scripts.Gameplay.ObjectActivity
{
    [CreateAssetMenu(fileName = "HideObjectActivity", menuName = "Create 'HideObjectActivity'", order = 0)]
    public class HideObjectActivity : LevelItemBaseActivity
    {
        public float delay;

        public override void Run(ILevelItemView itemView)
        {
            itemView.HideAndDeactivate();
        }
    }
}