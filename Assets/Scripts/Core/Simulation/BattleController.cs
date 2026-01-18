using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services;
using UnityEngine;

namespace Core.Simulation
{
    public class BattleController : IDisposable, IBattleController
    {
        private readonly ITickableService tickService;
        private IEndGameService endGameService;
        private List<IUnit> armyA = new List<IUnit>();
        private List<IUnit> armyB = new List<IUnit>();
        private bool isSimulating = false;

        private readonly Dictionary<IUnit, IUnit> attackerToTarget = new Dictionary<IUnit, IUnit>();
        private readonly Dictionary<IUnit, int> targetAttackCount = new Dictionary<IUnit, int>();

        private const int MaxAttackersPerTarget = 3;
        private const float DistanceWeight = 0.7f;
        private const float BalanceWeight = 0.3f;

        public BattleController(ITickableService tickService, IEndGameService endGameService)
        {
            this.tickService = tickService;
            this.endGameService = endGameService;
            this.tickService.OnTick += OnTick;
        }

        public void StartSimulating(List<IUnit> armyA, List<IUnit> armyB)
        {
            this.armyA = armyA.Where(u => u != null).ToList();
            this.armyB = armyB.Where(u => u != null).ToList();

            SubscribeToDeaths(this.armyA);
            SubscribeToDeaths(this.armyB);

            UpdateTargets();

            isSimulating = true;
        }

        private void SubscribeToDeaths(List<IUnit> units)
        {
            foreach (var unit in units)
            {
                unit.OnDeath += OnUnitDeath;
            }
        }

        private void OnTick()
        {
            if (!isSimulating) return;
            UpdateTargets();
        }

        private void UpdateTargets()
        {
            attackerToTarget.Clear();
            targetAttackCount.Clear();

            DistributeTargets(armyA, armyB);
            DistributeTargets(armyB, armyA);

            ApplyTargetsToUnits();
        }

        private void DistributeTargets(List<IUnit> attackers, List<IUnit> enemies)
        {
            var aliveAttackers = attackers.Where(u => u != null).ToList();
            var aliveEnemies = enemies.Where(u => u != null).ToList();

            if (aliveEnemies.Count == 0) return;

            foreach (var attacker in aliveAttackers)
            {
                var bestTarget = FindBestTarget(attacker, aliveEnemies);
                if (bestTarget != null)
                {
                    attackerToTarget[attacker] = bestTarget;

                    if (!targetAttackCount.ContainsKey(bestTarget))
                        targetAttackCount[bestTarget] = 0;
                    targetAttackCount[bestTarget]++;
                }
            }
        }

        private IUnit FindBestTarget(IUnit attacker, List<IUnit> potentialTargets)
        {
            IUnit bestTarget = null;
            float bestScore = float.MinValue;

            foreach (var target in potentialTargets)
            {
                float score = CalculateTargetScore(attacker, target);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestTarget = target;
                }
            }

            return bestTarget;
        }

        private float CalculateTargetScore(IUnit attacker, IUnit target)
        {
            float distance = Vector3.Distance(
                attacker.Transform.position,
                target.Transform.position
            );
            float distanceScore = (1f / Math.Max(distance, 0.1f)) * DistanceWeight;

            int currentAttackers = targetAttackCount.ContainsKey(target) ?
                targetAttackCount[target] : 0;
            float balanceScore = (MaxAttackersPerTarget - currentAttackers) * BalanceWeight;

            return distanceScore + balanceScore;
        }

        private void ApplyTargetsToUnits()
        {
            foreach (var pair in attackerToTarget)
            {
                var attacker = pair.Key;
                var target = pair.Value;

                if (attacker is ICombatUnit combatUnit)
                {
                    combatUnit.SetTarget(target.Transform);
                }
            }
        }

        private void OnUnitDeath(IUnit deadUnit)
        {
            armyA.Remove(deadUnit);
            armyB.Remove(deadUnit);

            deadUnit.OnDeath -= OnUnitDeath;

            UpdateTargets();
            CheckBattleEnd();
        }

        private void CheckBattleEnd()
        {
            if (!isSimulating) return;

            if (armyA.Count == 0 && armyB.Count == 0)
            {
                Debug.Log("Draw");
                StopSimulation();
            }
            else if (armyA.Count == 0)
            {
                Debug.Log("ArmyBWins");
                StopSimulation();
            }
            else if (armyB.Count == 0)
            {
                Debug.Log("ArmyAWins");
                StopSimulation();
            }
        }

        private void StopSimulation()
        {
            isSimulating = false;
            endGameService.EndGame();
        }

        public void Dispose()
        {
            if (tickService != null)
            {
                tickService.OnTick -= OnTick;
            }

            foreach (var unit in armyA.Concat(armyB))
            {
                unit.OnDeath -= OnUnitDeath;
            }

            isSimulating = false;
        }
    }
}