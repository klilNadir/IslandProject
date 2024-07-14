using IslandProject.Models.Individual;

namespace IslandProject.Models.Functions
{
    public class BestDetails
    {
        public IIndividual Best { get; set; }
        public double BestValue { get; set; }
        public int BestValueChangeCount { get; set; } = 0;
        public int TimeFromLastChange { get; set; } = 0;
    }
}
