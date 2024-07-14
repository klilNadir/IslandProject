using IslandProject.Models.Individual;

namespace IslandProject.Models.Heuristics
{
    public interface IHeuristic
    {
        public  void Step();
        public void StepAfterMigration(List<IIndividual> inPopulation);
        public List<IIndividual> GetPopulation();
        public void SetPopulation(List<IIndividual> population);
    }
}
