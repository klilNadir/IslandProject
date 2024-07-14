using IslandProject.Models.Enums;
using IslandProject.Models.Individual;
using IslandProject.Models.Setup;

namespace IslandProject.Models.Functions
{
    public class Griewangk : FunctionBase
    {
        public Griewangk(int dimension)
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
            double value = 0,sum =0.0,prod=1.0;

            for (int i = 0; i < RunDetails.Dimension; i++)
            {
                sum += doubleIndividual.Nums[i] * doubleIndividual.Nums[i];
                prod *=Math.Cos( doubleIndividual.Nums[i]) / Math.Sqrt((double)(i +1.0));
            }
            value = 1.0 + sum / 4000.0 - prod;
            target.Value = value;
            CheckBestChange(value, doubleIndividual);
            return value;
        }
    }
}
