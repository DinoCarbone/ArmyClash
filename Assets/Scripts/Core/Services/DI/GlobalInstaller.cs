using System.Collections.Generic;
using Core.Simulation;
using Data.Dto;
using Data.ScriptableObjects.Configs;
using Data.ScriptableObjects.Configs.Formation;
using UnityEngine;
using Zenject;

namespace Core.Services.DI
{
    public class GlobalInstaller : MonoInstaller
    {
        // тестовые данные для запуска main сыены с готовыми данными. Переопределяются в Menu.
        [Header("Init Debug Configs"), Space(10)] 
        [SerializeField] private List<BaseMultiConfig> generalConfigA;
        [SerializeField] private List<BaseMultiConfig> generalConfigB;
        [SerializeField] private ShapeConfig shapeConfig;
        [SerializeField] private SizeConfig sizeConfig;
        [SerializeField] private MaterialConfig materialConfig;
        [SerializeField] private FormationConfig formationConfig;


        override public void InstallBindings()
        {
            BindGameFlowService();
            BindSimulationProvider();
        }

        private void BindGameFlowService()
        {
            Container.BindInterfacesTo<GameFlowService>().AsSingle();
        }

        private void BindSimulationProvider()
        {
            Container.BindInterfacesTo<SimulationProvider>().FromMethod(CreateSimulationProvider).AsSingle();
        }

        private SimulationProvider CreateSimulationProvider(InjectContext context)
        {
            var armyAUnits = new List<(int, List<BaseMultiConfig>)>
        {
            (5, new List<BaseMultiConfig> { shapeConfig, sizeConfig, materialConfig }),
            (5, new List<BaseMultiConfig> { shapeConfig, sizeConfig })
        };

            var armyA = new ArmyData(formationConfig, armyAUnits, generalConfigA);
            var armyBUnits = new List<(int, List<BaseMultiConfig>)>
        {
            (8, new List<BaseMultiConfig> {
                shapeConfig,
                sizeConfig,
                materialConfig,
            })
        };
            var armyB = new ArmyData(formationConfig, armyBUnits, generalConfigB);

            return new SimulationProvider(armyA, armyB);
        }
    }
}