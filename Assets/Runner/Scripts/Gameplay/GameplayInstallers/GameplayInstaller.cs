using Runner.Scripts.Gameplay.Character.Controllers;
using Runner.Scripts.Gameplay.Handlers;
using Runner.Scripts.Gameplay.Level;
using Runner.Scripts.Gameplay.Level.Effects.Controllers;
using Runner.Scripts.Gameplay.Level.LevelObjects.Controllers;
using Runner.Scripts.Gameplay.Level.LevelObjects.Handlers;
using Runner.Scripts.Gameplay.Level.Platform.Controllers;
using Runner.Scripts.Gameplay.Misc;
using Zenject;

namespace Runner.Scripts.Gameplay.GameplayInstallers
{
    /// <summary>
    /// Installs the bindings for the game's core gameplay systems into the dependency injection container.
    /// </summary>
    public class GameplayInstaller : MonoInstaller<GameplayInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SpawnCharacterController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelGenerateFacade>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterRunController>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterAnimationController>().AsSingle();

            //platforms
            Container.BindInterfacesAndSelfTo<LevelPlatformsLoadController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlatformsGenerateController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelStepTakeHandler>().AsSingle();

            //level objects
            Container.BindInterfacesAndSelfTo<LevelObjectsLoadController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelObjectsController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelObjectTriggerPlayerHandler>().AsSingle();

            //effects
            Container.BindInterfacesAndSelfTo<EffectPlayController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EffectsLoadController>().AsSingle();

            Container.BindInterfacesAndSelfTo<TouchAndMouseHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<JumpHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<RestoreRunAfterJumpHandler>().AsSingle();
        }
    }
}