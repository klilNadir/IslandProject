namespace IslandProject.Models.Random
{
    using IslandProject.Models.Setup;
    using System;
    public class SystemRandom : IRandom
    {
        private Random random;

        public SystemRandom()
        {
            random = new Random();
        }
    

        public bool[] NextBooleans()
        {
            int dimension =RunDetails.Dimension;
            Boolean[] result = new Boolean[dimension];
           
            for (int i = 0; i < dimension; i++)
            {
                result[i] = random.NextInt64(0, 1) == 0;
            }
            return result;
        }

        public double NextDouble(double lowerBound, double upperBound)
        {
            ;
            return random.NextDouble() *(upperBound -lowerBound) +lowerBound;
        }

        public double[] NextDoubles(double lowerBound, double upperBound)
        {
            int dimension = RunDetails.Dimension;
            double[] values = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                values[i] = random.NextDouble() * (upperBound - lowerBound) + lowerBound;
            }
            return values;
        }

        public int NextInt(int lowerBound, int upperBound)
        {
            
            return (int)random.Next(lowerBound, upperBound);
        }

        public int[] NextInts(int lowerBound, int upperBound)
        {
            
            int dimension = RunDetails.Dimension;
            int[] ints = new int[dimension];
            for (int i = 0;i < dimension; i++)
            {
                ints[i] = random.Next(lowerBound,upperBound);
            }
            return ints;

        }

    }
}
