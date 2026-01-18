using System.Collections.Generic;
using UnityEngine;

namespace Data.ScriptableObjects.Configs.Formation
{
    [CreateAssetMenu(fileName = "Triangle", menuName = "ScriptableObjects/Formation/Triangle")]
    public class TriangleFormation : FormationConfig
    {
        [Header("Triangle Settings")]
        [SerializeField] private bool inverted = false;

        public override List<Vector3> GetFormationData(int countUnits, Vector3 center)
        {
            var positions = new List<Vector3>();

            if (countUnits <= 0) return positions;

            if (inverted)
                return GetInvertedTrianglePositions(countUnits, center);
            else
                return GetRegularTrianglePositions(countUnits, center);
        }

        private List<Vector3> GetRegularTrianglePositions(int countUnits, Vector3 center)
        {
            var positions = new List<Vector3>();

            int row = 0;
            int placedUnits = 0;

            while (placedUnits < countUnits)
            {
                int unitsInRow = row + 1;

                if (placedUnits + unitsInRow > countUnits)
                {
                    unitsInRow = countUnits - placedUnits;
                }

                for (int i = 0; i < unitsInRow; i++)
                {
                    float xPosition = (i - (unitsInRow - 1) / 2f) * unitSpacing;
                    float zPosition = -row * rankSpacing;

                    positions.Add(center + new Vector3(xPosition, 0, zPosition));
                }

                placedUnits += unitsInRow;
                row++;
            }

            return positions;
        }

        private List<Vector3> GetInvertedTrianglePositions(int countUnits, Vector3 center)
        {
            var positions = new List<Vector3>();

            int maxRows = 0;
            int totalForMaxRows = 0;

            while (totalForMaxRows + (maxRows + 1) <= countUnits)
            {
                maxRows++;
                totalForMaxRows += maxRows;
            }

            int remainingUnits = countUnits - totalForMaxRows;
            int totalRows = maxRows + (remainingUnits > 0 ? 1 : 0);

            int placedUnits = 0;

            for (int row = 0; row < totalRows; row++)
            {
                int unitsInRow;

                if (row < maxRows)
                {
                    unitsInRow = maxRows - row;
                }
                else
                {
                    unitsInRow = remainingUnits;
                }

                for (int i = 0; i < unitsInRow; i++)
                {
                    float xPosition = (i - (unitsInRow - 1) / 2f) * unitSpacing;
                    float zPosition = -row * rankSpacing;

                    positions.Add(center + new Vector3(xPosition, 0, zPosition));
                    placedUnits++;
                }
            }

            return positions;
        }
    }
}