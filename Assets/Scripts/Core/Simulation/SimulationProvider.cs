using Data.Dto;

namespace Core.Simulation
{
    public class SimulationProvider : ISimulationProvider, ISimulationEditor
    {
        private  ArmyData armyA;
        private  ArmyData armyB;
        public ArmyData ArmyA => armyA;

        public ArmyData ArmyB => armyB;
        public SimulationProvider(ArmyData armyA, ArmyData armyB)
        {
            this.armyA = armyA;
            this.armyB = armyB;
        }

        public void SetArmyA(ArmyData army)
        {
            armyA = army;
        }

        public void SetArmyB(ArmyData army)
        {
            armyB = army;
        }
    }
}