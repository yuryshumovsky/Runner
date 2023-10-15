using System;
using UnityEngine;

namespace Runner.Scripts.Gameplay.Level.Platform.ViewElements
{
    public class PlatformKillElementView : MonoBehaviour
    {
        public Action OnKillSomeone;

        private void OnTriggerEnter(Collider other)
        {
            OnKillSomeone?.Invoke();
        }
    }
}