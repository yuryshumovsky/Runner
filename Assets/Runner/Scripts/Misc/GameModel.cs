using System.Collections.Generic;
using Runner.Scripts.Gameplay.Character.ViewElements;
using Runner.Scripts.Gameplay.Level.Effects.ViewElements;
using Runner.Scripts.Gameplay.Level.LevelObjects.ViewElements;
using Runner.Scripts.Gameplay.Level.Platform.ViewElements;

namespace Runner.Scripts.Misc
{
    public class GameModel
    {
        public CharacterView CurrentCharacter { get; set; }
        public Dictionary<string, PlatformView.Factory> levelPlatformFactoryDict = new();
        public Dictionary<string, LevelObjectView.Factory> levelObjectFactoryDict = new();
        public Dictionary<string, LevelEffectView.Factory> levelEffectsFactoryDict = new();

        public JumpState jumpState = JumpState.NONE;
    }
}