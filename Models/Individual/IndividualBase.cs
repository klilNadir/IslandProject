using IslandProject.Models.Setup;
using Microsoft.AspNetCore.Components.Forms;

namespace IslandProject.Models.Individual
{
    public abstract class IndividualBase : IIndividual
    {
        public int Grace { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int CompareTo(IIndividual? other)
        {
            if (this.Value == other.Value) return 0;
            int returnValue;
           if (this.Value > other.Value)
            {
                 returnValue = RunDetails.IsMax ? 1 : -1;
            }
            else
            {
                returnValue = RunDetails.IsMax ? -1 : 1;
            }
           return returnValue;
        }

        public  virtual int DiversityCheck(IIndividual other)
        {
            throw new NotImplementedException();
        }

        public  virtual IIndividual Mutate(List<int> dimensionToMutate, double mutationMaxDiffrence, bool createNew = false)
        {
            throw new NotImplementedException();
        }

        public string Print()
        {
            throw new NotImplementedException();
        }
    }
}
