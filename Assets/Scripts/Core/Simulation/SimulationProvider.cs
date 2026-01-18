using Data.Dto;

namespace Core.Simulation
{
    public class SimulationProvider : ISimulationProvider
    {
        private readonly ArmyData armyA;
        private readonly ArmyData armyB;
        public ArmyData ArmyA => armyA;

        public ArmyData ArmyB => armyB;
        public SimulationProvider(ArmyData armyA, ArmyData armyB)
        {
            this.armyA = armyA;
            this.armyB = armyB;
        }
    }
}