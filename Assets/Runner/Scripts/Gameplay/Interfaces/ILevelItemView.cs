using UnityEngine;

namespace Runner.Scripts.Gameplay.Interfaces
{
    /// <summary>
    /// An interface for level item views.
    /// </summary>
    public interface ILevelItemView
    {
        string ID { get; set; }
        void HideAndDeactivate();

        Vector3 Position { get; set; }
    }
}