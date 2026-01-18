using Core.Behaviors.Entities;
using Core.Behaviors.Lifecycle;
using Core.Simulation;
using UnityEngine;
using Zenject;

namespace Core.Services.DI
{
    public class MockInstaller : MonoInstaller
    {
        [Header("Prefubs"), Space(10)]
        [SerializeField] private GameObject enemyPrefub;
        [Header("Positions"), Space(10)]
        [SerializeField] private Vector3 positionArmyA;
        [SerializeField] private Vector3 positionArmyB;
        [SerializeField] private float quaternionYTeamA;
        [SerializeField] private float quaternionYTeamB;

        public override void InstallBindings()
        {
            BindTickableService();
            BindHybridInjectService();
            BindEntityFactory();
            BindSimulationFacade();
            BindBattleController();
            BindDestructionService();
        }

        private void BindTickableService()
        {
            Container.BindInterfacesTo<TickableService>().AsSingle();
        }

        private void BindHybridInjectService()
        {
            Container.Bind<IHybridInjectService>().To<HybridInjectService>().AsSingle();
        }

        private void BindEntityFactory()
        {
            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle().WithArguments(enemyPrefub);
        }

        private void BindSimulationFacade()
        {
            Quaternion rotationA = new Quaternion(0, quaternionYTeamA, 0, 1);
            Quaternion rotationB = new Quaternion(0, quaternionYTeamB, 0, 1);

            Container.Bind<IInitializable>().To<SimulationFacade>().AsSingle()
                .WithArguments(positionArmyA, positionArmyB, rotationA, rotationB).NonLazy();
        }

        private void BindBattleController()
        {
            Container.Bind<IBattleController>().To<BattleController>().AsSingle();
        }

        private void BindDestructionService()
        {
            Container.Bind<IDestructionService>().To<DestructionService>().AsSingle();
        }
    }
}