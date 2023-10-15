using UnityEngine;
using Zenject;

namespace Runner.Scripts.Configs
{
    /// <summary>
    /// Installs the game configuration scriptable object into the dependency injection container.
    /// </summary>
    [CreateAssetMenu(fileName = "ConfigScriptableObjectInstaller", menuName = "Installers/ConfigScriptableObjectInstaller")]
    public class ConfigScriptableObjectInstaller : ScriptableObjectInstaller<ConfigScriptableObjectInstaller>
    {
        [SerializeField] private GameConfig gameGameConfig;
    
        public override void InstallBindings()
        {
            Container.BindInstance(gameGameConfig);
        }
    }
}