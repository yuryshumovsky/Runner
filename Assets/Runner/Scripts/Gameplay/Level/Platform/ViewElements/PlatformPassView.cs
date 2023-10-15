using System;
using UnityEngine;

namespace Runner.Scripts.Gameplay.Level.Platform.ViewElements
{
    public class PlatformPassView : MonoBehaviour
    {
        public Action OnTriggerByPlayer;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerByPlayer?.Invoke();
        }
    }
}