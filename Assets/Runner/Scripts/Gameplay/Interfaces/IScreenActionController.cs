namespace Runner.Scripts.Gameplay.Interfaces
{
    /// <summary>
    /// An interface for screen action controllers.
    /// </summary>
    public interface IScreenActionController
    {
        void SingleDownOrTapHandler();

        void DoubleDownOrTapHandler();
    }
}