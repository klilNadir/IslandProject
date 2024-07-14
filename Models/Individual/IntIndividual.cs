using IslandProject.Models.Random;
using IslandProject.Models.Setup;

namespace IslandProject.Models.Individual
{
    public class IntIndividual : IndividualBase
    {
        public int Grace { get ; set  ; }
        public double Value { get ; set ; }
        public int[] Ints { get; set ; }
        
        public static int LowerBound { get; set; }
        public static int UpperBound { get; set; }

        private IRandom random { get; set; }
    

    public IntIndividual(int grace, IRandom random)
        {
            Grace = grace;
            this.random = random;
            Ints = random.NextInts(LowerBound, UpperBound);

        }

        public IntIndividual(int grace, int[] values ,IRandom random)
        {
            Grace = grace;
            this.random = random;
            this.Ints = values;
        }

        public int CompareTo(IIndividual? other)
        {
            throw new NotImplementedException();
        }

        public string Print()
        {
            throw new NotImplementedException();
        }

        public IIndividual Mutate(List<int> dimensionToMutate, double mutationMaxDiffrence, bool createNew = false)
        {
            var ints = Ints;
            int maxdiffrence= (int) mutationMaxDiffrence;
            foreach (int i in dimensionToMutate) 
            {
                int newValue = random.NextInt(Math.Max((int)RunDetails.LowerBound, ints[i] - maxdiffrence), Math.Min((int)RunDetails.UpperBound, ints[i] + maxdiffrence));
                ints[i] = newValue;
            }
            if (createNew)
            {
                return new IntIndividual(0, ints, random);
            }
            return this;
        }
        public override int DiversityCheck(IIndividual other)
        {
            int diversity = 0;
            var IntOther = other as IntIndividual;
            for (int i = 0; i < RunDetails.Dimension; i++)
            {
                diversity += Math.Abs(Ints[i] - IntOther.Ints[i]);
            }
            double range = RunDetails.UpperBound - RunDetails.LowerBound;
            return (int)(diversity / range);
        }


    }
}
