using IslandProject.Models.Enums;
using IslandProject.Models.Random;

namespace IslandProject.Models.Individual
{
    public interface IIndividual : IComparable<IIndividual>
    {
        public int Grace { get; set; }
        public static FunctionType FunctionType { get; }
        public double Value { get; set; }
        public static bool IsMax { get; set; }



        public IIndividual Mutate(List<int> dimensionToMutate, double mutationMaxDiffrence, bool createNew = false);

        public int DiversityCheck(IIndividual other);
        public string Print();
    }
}
