using System;

namespace Runner.Scripts.Gameplay.Interfaces
{
    /// <summary>
    /// An interface for level generators.
    /// </summary>
    public interface ILevelGenerator
    {
        Action<ILevelGenerator, ILevelItemView> OnTake { get; set; }
        bool DefineLevelStep { get; }
        void DeleteLastOne();
        float GenerateNewOne(float position);
    }
}