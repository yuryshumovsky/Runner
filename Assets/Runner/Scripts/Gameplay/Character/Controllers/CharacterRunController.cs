using Runner.Scripts.Misc;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Character.Controllers
{
    /// <summary>
    /// Controls the movement of the game's character when running.
    /// </summary>
    public class CharacterRunController : ITickable
    {
        [Inject] private GameModel _gameModel;
        private bool _enable = false;

        public bool Enable
        {
            set { _enable = value; }
        }

        public void Tick()
        {
            if (_enable)
            {
                float shift = 2.5f * SpeedFactor * Time.deltaTime;
                _gameModel.CurrentCharacter.Position += new Vector3(0, 0, shift);
            }
        }

        public float SpeedFactor { set; get; } = 1;
    }
}