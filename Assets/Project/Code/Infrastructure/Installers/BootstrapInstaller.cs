using Code.Gameplay.Common.Random;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Input.Service;
using Code.Gameplay.LevelGenerator.Factories;
using Code.Gameplay.LevelGenerator.Systems;
using Code.Gameplay.Levels;
using Code.Gameplay.Player.Factories;
using Code.Gameplay.Player.Systems;
using Code.Gameplay.ScoreCounter.Systems;
using Code.Gameplay.StaticData;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Provider;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        public override void InstallBindings()
        {
            BindInputService();
            BindProgressServices();
            BindStateFactory();
            BindStateMachine();
            BindGameStates();
            BindGameplayFactories();
            BindInfrastructureServices();
            BindAssetManagementServices();
            BindCommonServices();
            BindGameplayServices();
            BindGameplaySystems();
        }
        
        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<InitializeProgressState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingMainMenuScreenState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuScreenState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingGameplayState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayEnterState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayWaitForKeyState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameloopLoopState>().AsSingle();
        }


        private void BindGameplaySystems()
        {
            Container.Bind<IPlayerMoverSystem>().To<PlayerMoverSystem>().AsSingle();
            Container.Bind<ILevelGeneratorSystem>().To<LevelGeneratorSystem>().AsSingle();
            Container.Bind<IObstacleGeneratorSystem>().To<ObstacleGeneratorSystem>().AsSingle();
            Container.Bind<IScoreCounterSystem>().To<ScoreCounterSystem>().AsSingle();
            Container.Bind<IPassedObstaclesCounterSystem>().To<PassedObstaclesCounterSystem>().AsSingle();
        }

        private void BindGameplayFactories()
        {
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ILevelDataProvider>().To<LevelDataProvider>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindAssetManagementServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindProgressServices()
        {
            Container.Bind<IProgressProvider>().To<ProgressProvider>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void BindInputService()
        {
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}