using System.Collections.Generic;
using Core.Behaviors.Entities;
using Core.Behaviors.Lifecycle;
using Core.Simulation;
using Data.Dto;
using Data.ScriptableObjects.Configs;
using Data.ScriptableObjects.Configs.Formation;
using UnityEngine;
using Zenject;

namespace Core.Services.DI
{
    public class MockInstaller : MonoInstaller
    {
        [SerializeField] private GameObject enemyPrefub;
        [SerializeField] private ShapeConfig shapeConfig;
        [SerializeField] private SizeConfig sizeConfig;
        [SerializeField] private MaterialConfig materialConfig;
        [SerializeField] private FormationConfig formationConfig;
        [SerializeField] private Vector3 positionArmyA;
        [SerializeField] private Vector3 positionArmyB;

        override public void InstallBindings()
        {
            Container.BindInterfacesTo<TickableService>().AsSingle();

            Container.Bind<IHybridInjectService>().To<HybridInjectService>().AsSingle();

            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle().WithArguments(enemyPrefub);

            Container.Bind<IInitializable>().To<SimulationFacade>().AsSingle().WithArguments(positionArmyA, positionArmyB).NonLazy();

            Container.Bind<IBattleController>().To<BattleController>().AsSingle();

            Container.Bind<IDestructionService>().To<DestructionService>().AsSingle();

            Container.Bind<ISimulationProvider>().To<SimulationProvider>().FromMethod(ResolveSimulationProvider).AsSingle();

        }

        private SimulationProvider ResolveSimulationProvider(InjectContext context)
        {
            // Создаем тестовые армии

            // Армия A - 10 воинов с конфигами
            var armyAUnits = new List<(int, List<BaseMultiConfig>)>
        {
            (5, new List<BaseMultiConfig> { shapeConfig, sizeConfig, materialConfig }), // 5 воинов
            (5, new List<BaseMultiConfig> { shapeConfig, sizeConfig }) // еще 5 воинов
        };

            var armyA = new ArmyData(formationConfig, armyAUnits);

            // Армия B - 8 воинов с конфигами
            var armyBUnits = new List<(int, List<BaseMultiConfig>)>
        {
            (8, new List<BaseMultiConfig> {
                shapeConfig,
                sizeConfig,
                materialConfig,
            })
        };

            var armyB = new ArmyData(formationConfig, armyBUnits);

            return new SimulationProvider(armyA, armyB);
        }
    }
}