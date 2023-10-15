using Runner.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace Runner.Scripts.Gameplay.ObjectActivity
{
    public abstract class LevelItemBaseActivity : ScriptableObject
    {
        abstract public void Run(ILevelItemView itemView);
    }
}