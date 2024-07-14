using IslandProject.Models.Functions;
using IslandProject.Models.Individual;
using IslandProject.Models.Random;
using IslandProject.Models.Setup;

namespace IslandProject.Models.Heuristics
{
    //https://en.wikipedia.org/wiki/Particle_swarm_optimization
    //https://www.youtube.com/watch?v=Tgtuyr6hh-c
    public class BasicPso : IHeuristic
    {
        public double W = 0.7298;
        public double phil1 = 1.49618;
        public double phil2 = 1.49618;
        public int MaxVelocity = 2;
        public int StepSize { get; set; } = 1;
        public int PopulationSize { get; set; } = 20;

        private DoubleIndividual[] population ;
        private double[,] personalBest ;
        private double[,] velocity;
        private double[] personalBestValues;
        private double[] globalBest;
        private double globalBestValue;

        private IRandom random;
        private IFunction function;

        public BasicPso(IRandom random, IFunction function)
        {
            int dimension = RunDetails.Dimension;
            bool ismax = RunDetails.IsMax;
            population = new DoubleIndividual[PopulationSize];
            personalBest = new double[PopulationSize, dimension];
            velocity = new double[PopulationSize, dimension];
            globalBest = new double[dimension];
            this.random = random;
            this.function = function;

            FirstStep();
        }
        public void FirstStep() 
        {
            int dimension = RunDetails.Dimension;
            bool ismax = RunDetails.IsMax;
            globalBestValue= ismax  ? Double.MinValue : Double.MaxValue;
            int globalBestIndex = 0;
            for (int i = 0; i < PopulationSize; i++)
            {
                var values = random.NextDoubles(RunDetails.LowerBound, RunDetails.UpperBound);
                DoubleIndividual newIndividual = new DoubleIndividual(0, values);
                function.Eval(newIndividual);
                population[i] = newIndividual;
                personalBestValues[i] =newIndividual.Value;
                var startingVelocity =random.NextDoubles(-1 *MaxVelocity, MaxVelocity);
                for(int j = 0;j<dimension;j++) 
                {
                    velocity[i,j] = startingVelocity[j];
                    personalBest[i,j] = values[j];
                }
                 if((ismax && newIndividual.Value > globalBestValue) || (!ismax && newIndividual.Value < globalBestValue))
                {
                    globalBestValue = newIndividual.Value;
                    globalBestIndex = i;
                }
               
            }
            globalBest = population[globalBestIndex].Nums;

             
        }

        public void Step()
        {
            double[] newValues;
            
           for (int s = 0;s <StepSize;s++)
            {
                for(int p = 0;p < PopulationSize; p++)
                {
                    newValues = population[p].Nums;
                    double phil1val = random.NextDouble(0,phil1);
                    double phil2val = random.NextDouble(0,phil2);
                    for(int i = 0;i < RunDetails.Dimension; i++)
                    {
                        velocity[p, i] = (W * velocity[p, i] + (phil1val * (personalBest[p, i] - newValues[i])) + (phil2val * (globalBest[i] - newValues[i])));
                        newValues[i] = newValues[i] + velocity[p,i];
                    }
                    population[p].Nums = newValues;
                    function.Eval(population[p]);
                    CheckAndUpdateBests(population[p],p);


                }
            }
        }

        private void CheckAndUpdateBests(DoubleIndividual doubleIndividual, int index)
        {
            // new result is not personal best
            if ( (RunDetails.IsMax && doubleIndividual.Value < personalBestValues[index] )|| (!RunDetails.IsMax && doubleIndividual.Value > personalBestValues[index]))
            {
                return;
            }
            personalBestValues[index] = doubleIndividual.Value;
            for(int i = 0;i< RunDetails.Dimension; i++)
            {
                personalBest[index,i] = doubleIndividual.Nums[i];
            }
            //check global best
            if ((RunDetails.IsMax && doubleIndividual.Value < globalBestValue) || (!RunDetails.IsMax && doubleIndividual.Value > globalBestValue))
            {
                return;
            }
            globalBestValue = doubleIndividual.Value;
            globalBest =doubleIndividual.Nums;
        }

        public void StepAfterMigration(List<IIndividual> inPopulation)
        {
            throw new NotImplementedException();
        }

        public List<IIndividual> GetPopulation()
        {
            throw new NotImplementedException();
        }

        public void SetPopulation(List<IIndividual> population)
        {
            throw new NotImplementedException();
        }
    }
}
