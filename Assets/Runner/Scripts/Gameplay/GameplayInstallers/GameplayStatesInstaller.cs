using Runner.Scripts.Gameplay.GameplayStates;
using Zenject;

namespace Runner.Scripts.Gameplay.GameplayInstallers
{
    /// <summary>
    /// A MonoInstaller that installs the GameplayStates system.
    /// </summary>
    public class GameplayStatesInstaller : MonoInstaller<GameplayStatesInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplayStateManager>().AsSingle();

            Container.Bind<GameplayGenerateLevelState>().AsSingle();
            Container.Bind<GameplayCoreState>().AsSingle();
        }
    }
}