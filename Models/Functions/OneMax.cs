using IslandProject.Models.Enums;
using IslandProject.Models.Individual;
using IslandProject.Models.Setup;

namespace IslandProject.Models.Functions
{
    public class OneMax :FunctionBase
    {
        public OneMax(int dimension ) 
        {
            RunDetails.UpperBound = 1;
            RunDetails.LowerBound = 0;
            RunDetails.IsMax = true;
            RunDetails.CallCount = 0;
            RunDetails.Dimension = dimension;
            RunDetails.FunctionType = FunctionType.BOOLEAN;

        }

        public override double Eval(IIndividual target)
        {
            RunDetails.CallCount++;
            if (target.GetType() != typeof(BooleanIndividual)){
                return 0;
            }
            var booleanIndividual = target as BooleanIndividual;
            int value = 0;
            for (int i = 0; i <RunDetails.Dimension; i++)
            {
                if (booleanIndividual.Bools[i])
                {
                    value++;
                }
            }
            target.Value = value;
            CheckBestChange(value, booleanIndividual);
            return value;
        }
    }
}
