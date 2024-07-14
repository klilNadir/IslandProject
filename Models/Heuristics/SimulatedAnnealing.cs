using IslandProject.Models.Heuristics;
using IslandProject.Models.Individual;

namespace IslandProject.Models.Heuristic
{
    //https://www.youtube.com/watch?v=AEeYp5VtI08
    public class SimulatedAnnealing : IHeuristic
    {
        private double temprature;
        private double tempratureCoolingFactor;
        private int rejectCounter = 0;
        private int acceptCounter = 0;
        private int maxAcceptCounter = 10;
        private int maxRejectCounter =100;

        private IIndividual currentIndividual;
        private List<IIndividual> bacupTargets;
        public int StartingTemprature {  get; set; }
        
        public void Step()
        {
            throw new NotImplementedException();
        }

        public void StepAfterMigration(List<IIndividual> inPopulation)
        {
            throw new NotImplementedException();
        }
    }
}
