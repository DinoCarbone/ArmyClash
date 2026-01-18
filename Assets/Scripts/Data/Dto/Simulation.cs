using System.Collections.Generic;
using Data.ScriptableObjects.Configs;
using Data.ScriptableObjects.Configs.Formation;

namespace Data.Dto
{
    public class ArmyData
    {
        private FormationConfig formationConfig;
        private List<(int, List<BaseMultiConfig>)> unitData; 
        public FormationConfig FormationConfig => formationConfig;
        public List<(int, List<BaseMultiConfig>)> UnitData => unitData;
        public ArmyData(FormationConfig formationConfig, List<(int, List<BaseMultiConfig>)> unitData)
        {
            this.formationConfig = formationConfig;
            this.unitData = unitData;
        }
    }
}