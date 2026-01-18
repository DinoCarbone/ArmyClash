using System.Collections.Generic;
using UnityEngine;

namespace Data.ScriptableObjects.Configs.Formation
{
    public abstract class FormationConfig : ScriptableObject
    {
        [Header("Base Settings")]
        [SerializeField] protected float unitSpacing = 2f;       // Расстояние между юнитами в шеренге
        [SerializeField] protected float rankSpacing = 2f;       // Расстояние между рядами юнитов

        public abstract List<Vector3> GetFormationData(int countUnits, Vector3 center);
    }
}