using Core.Behaviors.Entities;
using Core.Providers;
using Data.ScriptableObjects.Configs;
using UnityEngine;
using Zenject;

namespace Core.Services.DI
{
    public class MockInstaller : MonoInstaller
    {
        [SerializeField] private TargetSceneProvider targetSceneProvider;
        [SerializeField] private GameObject enemyPrefub;
        [SerializeField] private ShapeConfig shapeConfig;
        [SerializeField] private SizeConfig sizeConfig;
        override public void InstallBindings()
        {
            Container.Bind<ITargetSceneProvider>().To<TargetSceneProvider>().FromInstance(targetSceneProvider).AsSingle();
            Container.Bind<IHybridInjectService>().To<HybridInjectService>().AsSingle();
            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle().WithArguments(enemyPrefub);
        }
        public override void Start()
        {
            base.Start();
            IEntityFactory entityFactory = Container.Resolve<IEntityFactory>();
            entityFactory.Create(shapeConfig, sizeConfig);
        }
    }
}