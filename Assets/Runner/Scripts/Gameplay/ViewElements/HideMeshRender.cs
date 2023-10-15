using UnityEngine;

namespace Runner.Scripts.Gameplay.ViewElements
{
    public class HideMeshRender : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}