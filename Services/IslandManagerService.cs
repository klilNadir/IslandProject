using IslandProject.Models;
using IslandProject.Models.Functions;
using IslandProject.Models.Graph;
using IslandProject.Models.Individual;
using IslandProject.Models.Setup;

namespace IslandProject.Services
{
    public class IslandManagerService
    {

        public int TimeLimit { get; set; }
        public int MaxCallCount { get; set; }
        public  Graph MigrationGraph { get; set; }

        private IFunction function;
        private int time;
        private List<Island> islands;
        private int islandsCount;
        private int MigrationGap;


        public void Start()
        {
            time = 1;
            while (time < TimeLimit && RunDetails.CallCount < MaxCallCount)
            {
                if (time % MigrationGap == 0)
                {
                    Migrate();
                }
                else
                {
                    foreach (var island in islands)
                    {
                        island.Step(false);
                    }
                }
            }
        }

        private void Migrate()
        {
            List<IIndividual>[] inMigration = new List<IIndividual>[islandsCount];
            for (int i = 0; i < islandsCount; i++)
            {
                inMigration[i] = new List<IIndividual>();
            }

                for (int i = 0; i < islandsCount; i++)
            {
                inMigration[i] = new List<IIndividual>();
                int maxOutmigration = MigrationGraph.FindToNighborsMaxWeight(i);
                var outMigrationPopulation = islands[i].GetIndividualsForMigration(maxOutmigration);
                outMigrationPopulation.Sort();
                var migrationEdges = MigrationGraph.FindToNighbors(i);

                foreach (var edge in migrationEdges)
                {
                    inMigration[edge.To].AddRange(outMigrationPopulation.Take(edge.Weight));
                }
            }

            for (int i = 0;i < islandsCount;i++)
            {
                islands[i].MigrationStep(inMigration[i]);
            }
        }
    }
}
