using Runner.Scripts.AppStates;
using Runner.Scripts.AppStates.Services;
using Runner.Scripts.Configs;
using Runner.Scripts.Misc;
using Runner.Scripts.Misc.Loader;
using Runner.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [Inject] private readonly GameConfig _gameConfig;

        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            

            InstallHelpers();

            Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.Bind<LoadingScreen>().FromComponentInNewPrefab(_gameConfig.loadingScreenPrefab).AsSingle().Lazy();

            InitStates();
        }

        private void InstallHelpers()
        {
            Container.Bind<CoroutineHelper>().FromNewComponentOnNewGameObject().AsSingle();
        }

        private void InitStates()
        {
            Container.Bind<AppLoadingAndInitState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();

            Container.BindInterfacesAndSelfTo<AppStateManager>().AsSingle();
        }
    }
}