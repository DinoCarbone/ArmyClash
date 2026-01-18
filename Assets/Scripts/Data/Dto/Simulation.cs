using System.Collections.Generic;
using Data.ScriptableObjects.Configs;
using Data.ScriptableObjects.Configs.Formation;

namespace Data.Dto
{
    public class ArmyData
    {
        private FormationConfig formationConfig;
        private List<(int, List<BaseMultiConfig>)> unitData;

        private List<BaseMultiConfig> generalConfigs;

        public FormationConfig FormationConfig => formationConfig;
        public List<(int, List<BaseMultiConfig>)> UnitData => unitData;
        public List<BaseMultiConfig> GeneralConfigs => generalConfigs;
        
        public ArmyData(FormationConfig formationConfig, List<(int, List<BaseMultiConfig>)> unitData,
         List<BaseMultiConfig> generalConfigs)
        {
            this.formationConfig = formationConfig;
            this.unitData = unitData;
            this.generalConfigs = generalConfigs;
        }
    }
}