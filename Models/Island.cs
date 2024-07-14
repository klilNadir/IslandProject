using IslandProject.Models.Enums;
using IslandProject.Models.Functions;
using IslandProject.Models.Heuristics;
using IslandProject.Models.Individual;
using IslandProject.Models.Random;
using IslandProject.Models.Setup;

namespace IslandProject.Models
{
    public class Island
    {
        public IHeuristic Heuristic;
        private IFunction function;
        private IRandom random;

        public void Step(bool print)
        {
            Heuristic.Step();

        }

        public void MigrationStep(List<IIndividual> inMigratingIndividuals)
        {
            Heuristic.StepAfterMigration(inMigratingIndividuals);
        }

        public List<IIndividual> GetIndividualsForMigration(int migrationSize)
        {
            List<IIndividual> individualsForMigration = new List<IIndividual>();
            List<IIndividual> population = Heuristic.GetPopulation();
            population.Sort();
            switch (MigrationDetails.MigrationCriteria)
            {
                case MigrationCriteria.Value:
                    individualsForMigration = population.Take(migrationSize).ToList();
                    break;
                case MigrationCriteria.Diversity:
                    individualsForMigration = FindBestDiversity(population, migrationSize);
                    break;
            }
            if(MigrationDetails.MigrationType ==MigrationType.erase)
            {
                population= population.Except(individualsForMigration).ToList();
                Heuristic.SetPopulation(population);
            }
            return individualsForMigration;
        }

        public List<IIndividual> FindBestDiversity(List<IIndividual> sortedPopulation, int migrationSize)
        {
            int currentDiversity = 0;
            bool acceptedFlag = false;
            List<IIndividual> individualsForMigration = new List<IIndividual>();
            //take any that is at least 5% diffrent
            int acceptedDiversity = RunDetails.Dimension / 20;
            individualsForMigration.Add(sortedPopulation.First());

            for (int i = 0; i < sortedPopulation.Count && individualsForMigration.Count <migrationSize; i++)
            {

                acceptedFlag = true;
                foreach (IIndividual individual in individualsForMigration)
                {
                    currentDiversity = individual.DiversityCheck(sortedPopulation[i]);
                    if (currentDiversity < acceptedDiversity)
                    {
                        acceptedFlag = false;
                        break;
                    }
                }
                if (acceptedFlag)
                {
                    individualsForMigration.Add((IIndividual)sortedPopulation[i]);
                }

            }
            if(individualsForMigration.Count < migrationSize) 
            {
                var targetPopulations = sortedPopulation.Except(individualsForMigration);
                individualsForMigration.AddRange(targetPopulations.Take(migrationSize - individualsForMigration.Count).ToList());
            }
            return individualsForMigration;
        }
    }
}
