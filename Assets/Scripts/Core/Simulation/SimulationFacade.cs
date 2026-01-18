using System.Collections.Generic;
using System.Linq;
using Core.Behaviors.Entities;
using Data.Dto;
using Data.ScriptableObjects.Configs.Formation;
using UnityEngine;
using Zenject;

namespace Core.Simulation
{
    public class SimulationFacade : IInitializable
    {
        private readonly IBattleController battleController;
        private readonly IEntityFactory entityFactory;
        private readonly ISimulationProvider simulationProvider;
        private readonly Vector3 positionArmyA;
        private readonly Vector3 positionArmyB;
        private readonly Quaternion quaternionA;
        private readonly Quaternion quaternionB;

        public SimulationFacade(IBattleController battleController, IEntityFactory entityFactory,
         ISimulationProvider simulationProvider, Vector3 positionArmyA, Vector3 positionArmyB, 
         Quaternion quaternionA, Quaternion quaternionB)
        {
            this.battleController = battleController;
            this.entityFactory = entityFactory;
            this.positionArmyA = positionArmyA;
            this.positionArmyB = positionArmyB;
            this.simulationProvider = simulationProvider;
            this.quaternionA = quaternionA;
            this.quaternionB = quaternionB;
        }

        public void Initialize()
        {
            SpawnArmies(simulationProvider.ArmyA, simulationProvider.ArmyB);
        }

        private void SpawnArmies(ArmyData armyA, ArmyData armyB)
        {
            List<IUnit> armyAUnits = SpawnArmy(armyA, positionArmyA, quaternionA);
            List<IUnit> armyBUnits = SpawnArmy(armyB, positionArmyB, quaternionB);
            battleController.StartSimulating(armyAUnits, armyBUnits);
        }
        private List<IUnit> SpawnArmy(ArmyData army, Vector3 centerPosition, Quaternion quaternion)
        {
            FormationConfig formationConfig = army.FormationConfig;
            int unitCount = army.UnitData.Sum(u => u.Item1);
            

            List<Vector3> createPoints = formationConfig.GetFormationData(unitCount, centerPosition);
            List<IUnit> spawnedUnits = new List<IUnit>();

            foreach (var (count, unitConfigs) in army.UnitData)
            {
                for (int i = 0; i < count; i++)
                {
                    unitConfigs.AddRange(army.GeneralConfigs); // Добавляем общие конфиги

                    Vector3 spawnPosition = createPoints[spawnedUnits.Count];
                    var entity = entityFactory.Create(spawnPosition, quaternion, unitConfigs.ToArray());
                    IUnit unit = entity.GetComponentInChildren<EntityBase>()?.GetModel<IUnit>();

                    if(unit != null)
                    {
                        spawnedUnits.Add(unit);
                    }
                    else
                    {
                        Debug.LogError("Unit component not found on spawned entity");
                    }
                }
            }

            return spawnedUnits;
        }

    }
}