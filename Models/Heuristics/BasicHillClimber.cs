using IslandProject.Models.Functions;
using IslandProject.Models.Individual;
using IslandProject.Models.Random;
using IslandProject.Models.Setup;
using System.Linq;

namespace IslandProject.Models.Heuristics
{
    //https://en.wikipedia.org/wiki/Hill_climbing
    public class BasicHillClimber :IHeuristic
    {
        public int ActionsInStep { get; set; }
        public double MaxDistanceFromTarget { get; set; }
        public int SwitchTargetCount { get; set; }
        public int MaxSwitchTargetCount { get; set; }
        public List<IIndividual> BacupTargets { get; set; }
        public IIndividual target { get; set; }
        public List<IIndividual> OutMigrationTargets { get; set; }
        public int TargetOutMigrationCount { get; set; }


        private IFunction Function;
        private IndividualFactory individualFactory;
        private IRandom random;

        public BasicHillClimber(IndividualFactory individualFactory, IFunction function, IRandom random)
        {
            this.individualFactory = individualFactory;
            Function = function;
            this.random = random;
            BacupTargets = new List<IIndividual>();
            ActionsInStep = 100;
            MaxSwitchTargetCount = 1000;

            FirsStep();
            
        }

        private void FirsStep()
        {
            target = individualFactory.CrateRandomIndividual(0);
            Function.Eval(target);
        }
        public void Step()
        {
            IIndividual testIndividual;
            int dimensionToChange;

            for (int i = 0; i < ActionsInStep; i++)
            {
                dimensionToChange = random.NextInt(0, RunDetails.Dimension - 1);
                testIndividual = target.Mutate(new List<int>() { dimensionToChange }, MaxDistanceFromTarget, true);
                Function.Eval(testIndividual);

                if (testIndividual.CompareTo(target) > 0)
                {
                    target = testIndividual;
                    SwitchTargetCount = 0;
                }
                else
                {
                    SwitchTargetCount++;
                    if (SwitchTargetCount >= MaxSwitchTargetCount && target.Grace < 0)
                    {
                        SwitchTargetCount = 0;
                        OutMigrationTargets.Add(target);
                        if (BacupTargets.Count > 0)
                        {
                            target = BacupTargets[0];
                            BacupTargets.RemoveAt(0);

                        }
                        else
                        {
                            target = individualFactory.CrateRandomIndividual(0);
                        }
                    }
                }
            }
            if(OutMigrationTargets.Count < TargetOutMigrationCount && !OutMigrationTargets.Contains(target))
            {
                OutMigrationTargets.Add(target);
            }
            target.Grace--;
        }

        public void StepAfterMigration(List<IIndividual> inPopulation)
        {
            if(inPopulation != null && inPopulation.Count >0)
            {
                SwitchTargetCount = 0;
                BacupTargets.AddRange(inPopulation);
                BacupTargets.Sort();
                BacupTargets.Take(ActionsInStep / 10);
            }

            Step();
        }

        public List<IIndividual> GetPopulation()
        {
            OutMigrationTargets.Add(target);
            return OutMigrationTargets;
        }

        public void SetPopulation(List<IIndividual> population)
        {
            target = population[0];
            SwitchTargetCount = 0;
            BacupTargets.AddRange(population);
            BacupTargets.Sort();
            BacupTargets.Take(ActionsInStep / 10);
        }
    }
}
