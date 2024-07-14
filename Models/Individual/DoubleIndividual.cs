using IslandProject.Models.Functions;
using IslandProject.Models.Random;
using IslandProject.Models.Setup;

namespace IslandProject.Models.Individual
{
    public class DoubleIndividual : IndividualBase
    {
        public int Grace { get ; set; }
        public double Value { get; set; }

        public double[] Nums { get; set; }

        public DoubleIndividual(int grace,IRandom random) 
        {
            Grace = grace;
        }
        public DoubleIndividual(int grace, double[] nums)
        {
            Grace = grace;
            Nums = nums;
        }

        public IIndividual GetRandomIndividual(int grace, IRandom random)
        {
            throw new NotImplementedException();
        }

        public string Print()
        {
            throw new NotImplementedException();
        }

        public override int DiversityCheck(IIndividual other)
        {
            double diversity = 0;
            var DoubleOther = other as DoubleIndividual;
            for(int i = 0; i<RunDetails.Dimension; i++)
            {
                diversity += Math.Abs(Nums[i] - DoubleOther.Nums[i]);
            }
            double range = RunDetails.UpperBound - RunDetails.LowerBound;
            return  (int ) (diversity / range);
        }
    }
}
