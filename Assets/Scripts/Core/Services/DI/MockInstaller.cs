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
        [Header("Simulation Positions")]
        [SerializeField] private List<BaseMultiConfig> generalConfigA;
        [SerializeField] private List<BaseMultiConfig> generalConfigB;
        [SerializeField] private Vector3 positionArmyA;
        [SerializeField] private Vector3 positionArmyB;
        [SerializeField] private float quaternionYTeamA;
        [SerializeField] private float quaternionYTeamB;
        [Header("Mock")]
        [SerializeField] private Transform target;

        override public void InstallBindings()
        {
            Container.BindInterfacesTo<TickableService>().AsSingle();

            Container.Bind<IHybridInjectService>().To<HybridInjectService>().AsSingle();

            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle().WithArguments(enemyPrefub);

            Container.Bind<IInitializable>().To<SimulationFacade>().AsSingle().
            WithArguments(positionArmyA, positionArmyB, new Quaternion(0, quaternionYTeamA, 0, 1),
             new Quaternion(0, quaternionYTeamB, 0, 1)).NonLazy();

            Container.Bind<IBattleController>().To<BattleController>().AsSingle();

            Container.Bind<IDestructionService>().To<DestructionService>().AsSingle();
        }

        private void StartDebugBehavior()
        {
            IEntityFactory factory = Container.Resolve<IEntityFactory>();
            var unit = factory.Create(Vector3.zero, default, null).GetComponentInChildren<EntityBase>()?.GetModel<ICombatUnit>();
            unit?.SetTarget(target);
        }
    }
}