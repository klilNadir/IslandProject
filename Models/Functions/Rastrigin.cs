using IslandProject.Models.Enums;
using IslandProject.Models.Individual;
using IslandProject.Models.Setup;

namespace IslandProject.Models.Functions
{
    public class Rastrigin : FunctionBase
    {
        const double M_2_PI = 0.636619772367581343076;
        public Rastrigin(int dimension)
        {
            RunDetails.UpperBound = 100;
            RunDetails.LowerBound = -100;
            RunDetails.IsMax = false;
            RunDetails.CallCount = 0;
            RunDetails.Dimension = dimension;
            RunDetails.FunctionType = FunctionType.DOUBLE;

        }

        public override double Eval(IIndividual target)
        {
            RunDetails.CallCount++;
            if (target.GetType() != typeof(DoubleIndividual))
            {
                return double.MaxValue;
            }
            var doubleIndividual = target as DoubleIndividual;
            double value = 10 * RunDetails.Dimension;
            for (int i = 0; i < RunDetails.Dimension; i++)
            {
                value += doubleIndividual.Nums[i] * doubleIndividual.Nums[i] - 10.0 * Math.Cos(M_2_PI * doubleIndividual.Nums[i]);
            }
            target.Value = value;
            CheckBestChange(value, doubleIndividual);
            return value;
        }
    }
}
