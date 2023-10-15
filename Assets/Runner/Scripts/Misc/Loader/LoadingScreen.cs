using UnityEngine;

namespace Runner.Scripts.Misc.Loader
{
    public class LoadingScreen : MonoBehaviour
    {
        private void Start()
        {
            Visible = true;
        }

        public bool Visible
        {
            set { gameObject.SetActive(value); }
        }
    }
}