using System.Collections.Generic;
using UnityEngine;

namespace Data.ScriptableObjects.Configs.Formation
{
    [CreateAssetMenu(fileName = "Rectangle", menuName = "ScriptableObjects/Formation/Rectangle")]
    public class RectangleFormation : FormationConfig
    {
        [Header("Rectangle Settings")]
        [SerializeField] private int maxUnitsPerRow = 5;

        public override List<Vector3> GetFormationData(int countUnits, Vector3 center)
        {
            var positions = new List<Vector3>();

            if (countUnits <= 0) return positions;

            int rows = Mathf.CeilToInt((float)countUnits / maxUnitsPerRow);
            int unitsInLastRow = countUnits % maxUnitsPerRow;
            if (unitsInLastRow == 0) unitsInLastRow = maxUnitsPerRow;

            for (int row = 0; row < rows; row++)
            {
                int unitsInCurrentRow = (row == rows - 1) ? unitsInLastRow : maxUnitsPerRow;

                for (int unitInRow = 0; unitInRow < unitsInCurrentRow; unitInRow++)
                {
                    float xPosition = (unitInRow - (unitsInCurrentRow - 1) / 2f) * unitSpacing;
                    float zPosition = -row * rankSpacing;

                    positions.Add(center + new Vector3(xPosition, 0, zPosition));
                }
            }

            return positions;
        }
    }
}