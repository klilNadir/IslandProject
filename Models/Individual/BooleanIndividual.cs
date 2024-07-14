using IslandProject.Models.Enums;
using IslandProject.Models.Functions;
using IslandProject.Models.Random;
using System.Security.Cryptography;


namespace IslandProject.Models.Individual
{
    public class BooleanIndividual : IndividualBase
    {
        public int Grace { get; set; }
        public static FunctionType FunctionType { get;  set; }
        public double Value { get; set ; }
        public Boolean[] Bools { get; set; }

        public BooleanIndividual(int grace,IRandom random)
        {
            Grace = grace;
            Bools =random.NextBooleans ();
        }
        public BooleanIndividual(int grace, bool[] bools)
        {
            Grace = grace;
            Bools = bools;
        }

        public int CompareTo(IIndividual? other)
        {
            throw new NotImplementedException();
        }

        public string Print()
        {
            throw new NotImplementedException();
        }

        public IIndividual GetRandomIndividual(int grace, IRandom random)
        {
            throw new NotImplementedException();
        }

        public IIndividual Mutate(List<int> dimensionToMutate, double mutationMaxDiffrence, bool createNew = false)
        {
            var bools =this.Bools;

            foreach (int i in dimensionToMutate)
            {
                bools[i] = !bools[i];
            }
            if (createNew)
            {
                return new BooleanIndividual(0, bools);
            }
            return this;
        }

        public int DiversityCheck(IIndividual other)
        {
           var booleanOther = other as BooleanIndividual;
           int diversity = 0;

            for (int i = 0;i <Bools.Count();i++)
            {
                if (Bools[i] != booleanOther.Bools[i])
                {
                    diversity++;
                }
            }

            return diversity;
        }
    }
}
