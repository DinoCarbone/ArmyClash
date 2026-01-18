using System;
using System.Collections.Generic;
using Data.Dto;
using UnityEngine;

namespace Core.Simulation
{
    public interface IUnit
    {
        Transform Transform { get; }
        event Action<IUnit> OnDeath;
    }
    public interface ICombatUnit : IUnit
    {
        void SetTarget(Transform target);
    }
    public interface IBattleController
    {
        void StartSimulating(List<IUnit> armyA, List<IUnit> armyB);
    }
    public interface ISimulationProvider
    {
        ArmyData ArmyA { get; }
        ArmyData ArmyB { get; }
    }
    public interface ISimulationEditor
    {
        void SetArmyA(ArmyData army);
        void SetArmyB(ArmyData army);
    }
}