using IslandProject.Models.Individual;

namespace IslandProject.Models.Heuristics
{
    public abstract class HeuristicBase : IHeuristic
    {
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
